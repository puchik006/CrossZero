using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class CrossZerroLobby : MonoBehaviour
{
    private Lobby _hostLobby;
    private float _heartbeatTimer;
    private string _playerName;

    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("ID signed: " + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        _playerName = "Bu" + UnityEngine.Random.Range(1, 99);

        Debug.Log(_playerName);

        CreateLobby();

    }

    private void Update()
    {
        HandleLobbyHeartbeat();
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
                Player = new Player
                {
                    //Id = AuthenticationService.Instance.PlayerId,
                    Data = new Dictionary<string, PlayerDataObject>
                    {
                        {"PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member,_playerName)} 
                    }
                }
            };

            var lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers,lobbyOptions);

            _hostLobby = lobby;

            PrintPlayers(_hostLobby);

            Debug.Log("Created lobby: " + lobby.Name + " max players " + lobby.MaxPlayers + " lobby ID: " + lobby.Id + " lobby code: " + lobby.LobbyCode);
        }
        catch (LobbyServiceException e)
        {

            Debug.Log(e);
        }
    }

    private async void ListLobbies()
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

    private async void JoinLobby()
    {
        try
        {
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();

            await Lobbies.Instance.JoinLobbyByIdAsync(queryResponse.Results[0].Id);
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
            await LobbyService.Instance.QuickJoinLobbyAsync();
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    private void PrintPlayers(Lobby lobby)
    {
        Debug.Log("Players in looby: " + lobby.Name);
        foreach (Player player in lobby.Players)
        {
            Debug.Log("Player: " + player.Id + " player name: " + player.Data["PlayerName"].Value);
        }
    }

    private async void LeaveLobby()
    {
        try
        {
            await LobbyService.Instance.RemovePlayerAsync(_hostLobby.Id, AuthenticationService.Instance.PlayerId);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

}
