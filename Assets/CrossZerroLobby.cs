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

    private const string KEY_START_GAME = "relayCode";

    private static event Action OnGameStart;

    [SerializeField] private CrossZeroRelay _relay; //Replace this with using of relay singletone

    public async void StartLobbyGame()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        _playerName = "Bu" + UnityEngine.Random.Range(1, 99); // take name from player settings

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
            PrintPlayers(querryResponce.Results[0]);
        }

        if (_joinLobby != null)
        {
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

                //if (_hostLobby.Players.Count == 2)
                //{
                //    StartGame();
                //}
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
                Player = GetPlayer(),
                Data = new Dictionary<string, DataObject> 
                {
                    {KEY_START_GAME, new DataObject(DataObject.VisibilityOptions.Member,"0") }
                }
            };

            var lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers,lobbyOptions);

            _hostLobby = lobby;
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
            Debug.Log("Player: " + player.Id + " player name: " + player.Data["PlayerName"].Value + " relay code "
                + lobby.Data[KEY_START_GAME].Value);
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

                //if (_joinLobby.Data[KEY_START_GAME].Value != "0")
                //{
                //    _relay.JoinRelay(_joinLobby.Data[KEY_START_GAME].Value);
                //}
            }
        }
    }

    public async void StartGame()
    {
        if (_hostLobby != null)
        {
            try
            {
                string relayCode = await _relay.CreateRelay();

                Lobby lobby = await Lobbies.Instance.UpdateLobbyAsync(_hostLobby.Id, new UpdateLobbyOptions 
                { 
                    Data = new Dictionary<string, DataObject>
                    {
                        {KEY_START_GAME,new DataObject(DataObject.VisibilityOptions.Member, relayCode)},
                    }
                });

                _hostLobby = lobby;

                //OnGameStart!.Invoke();

                Debug.Log("START GAME");
            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }
        }
    }
}
