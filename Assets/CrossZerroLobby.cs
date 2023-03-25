using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class CrossZerroLobby : MonoBehaviour
{
    private Lobby _hostLobby;
    private Lobby _joinLobby;
    private float _heartbeatTimer;
    private float _lobbyUpdateTimer;
    private string _playerName;

    public async void StartLobbyGame()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            //Debug.Log("ID signed: " + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        _playerName = "Bu" + UnityEngine.Random.Range(1, 99);

        //Debug.Log(_playerName);

        StartGameWithInternet();
    }

    private async void StartGameWithInternet()
    {
        try
        {
            var querryResponce = await Lobbies.Instance.QueryLobbiesAsync();

            if (querryResponce.Results.Count == 0)
            {
                CreateLobby();
                Debug.Log("Create lobby");
            }
            else
            {
                QuickJoinLobby();
                Debug.Log("Join lobby");
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }   
    }

    public async void PrintLobbyPlayers()
    {
        var querryResponce = await Lobbies.Instance.QueryLobbiesAsync();

        if (_hostLobby != null)
        {
            //PrintPlayers(_hostLobby);
            PrintPlayers(querryResponce.Results[0]);
        }

        if (_joinLobby != null)
        {
            //PrintPlayers(_joinLobby);
            PrintPlayers(querryResponce.Results[0]);
        }
    }

    private void Update()
    {
        HandleLobbyHeartbeat();
        HandleLobbyPollForUpdates();
    }

    private async void HandleLobbyHeartbeat()
    {
        if (_hostLobby != null)
        {
            _heartbeatTimer -= Time.deltaTime;
            if (_heartbeatTimer < 0f)
            {
                float heartbeatTimerMax = 15f;
                _heartbeatTimer = heartbeatTimerMax;

                await LobbyService.Instance.SendHeartbeatPingAsync(_hostLobby.Id);
            }
        }
    }

    private async void CreateLobby()
    {
        try
        {
            string lobbyName = "BuLobby";
            int maxPlayers = 2;
            CreateLobbyOptions lobbyOptions = new CreateLobbyOptions()
            {
                IsPrivate = false,
                Player = GetPlayer()
            };

            var lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers,lobbyOptions);

            _hostLobby = lobby;

            //Debug.Log("Created lobby: " + lobby.Name + " max players " + lobby.MaxPlayers + " lobby ID: " + lobby.Id + " lobby code: " + lobby.LobbyCode);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    public async void ListLobbies()
    {
        try
        {
            var querryResponce = await Lobbies.Instance.QueryLobbiesAsync();

            Debug.Log("Lobbies found " + querryResponce.Results.Count);

            foreach (var lobby in querryResponce.Results)
            {
                Debug.Log(lobby.Name + " " + lobby.MaxPlayers);
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    private async void QuickJoinLobby()
    {
        try
        {
            QuickJoinLobbyOptions quickJoinLobbyOptions = new QuickJoinLobbyOptions()
            {
                Player = GetPlayer()
            };

            var joinLobby = await LobbyService.Instance.QuickJoinLobbyAsync(quickJoinLobbyOptions);

            _joinLobby = joinLobby;
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    private void PrintPlayers(Lobby lobby)
    {
        Debug.Log("Players in looby: " + lobby.Name + " is " + lobby.Players.Count);
        foreach (Player player in lobby.Players)
        {
            //Debug.Log("Player: " + player.Id);
            Debug.Log("Player: " + player.Id + " player name: " + player.Data["PlayerName"].Value);
        }
    }

    public async void LeaveLobby()
    {
        try
        {
            if (_hostLobby != null)
            {
                await LobbyService.Instance.RemovePlayerAsync(_hostLobby.Id, AuthenticationService.Instance.PlayerId);
            }

            if (_joinLobby != null)
            {
                await LobbyService.Instance.RemovePlayerAsync(_joinLobby.Id, AuthenticationService.Instance.PlayerId);
            }
            
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    private Player GetPlayer()
    {
        return new Player
        {
            Data = new Dictionary<string, PlayerDataObject>
                    {
                        {"PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member,_playerName)}
                    }
        };
    }

    private async void HandleLobbyPollForUpdates()
    {
        if (_joinLobby != null)
        {
            _lobbyUpdateTimer -= Time.deltaTime;
            if (_lobbyUpdateTimer < 0f)
            {
                float lobbyUpdateTimerMax = 1.5f;
                _lobbyUpdateTimer = lobbyUpdateTimerMax;

                Lobby lobby = await LobbyService.Instance.GetLobbyAsync(_joinLobby.Id);
                _joinLobby = lobby;
            }
        }
    }

    private void OnApplicationQuit()
    {
        LeaveLobby();
    }
}
