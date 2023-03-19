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
    private Lobby hostLobby;

    private float heartbeatTimer;

    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("ID signed: " + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        CreateLobby();
    }

    private void Update()
    {
        HandleLobbyHeartbeat();
    }

    private async void HandleLobbyHeartbeat()
    {
        if (hostLobby != null)
        {
            heartbeatTimer -= Time.deltaTime;
            if (heartbeatTimer < 0f)
            {
                float heartbeatTimerMax = 15f;
                heartbeatTimer = heartbeatTimerMax;

                await LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
            }
        }

    }

    private async void CreateLobby()
    {
        try
        {
            string lobbyName = "BuLobby";
            int maxPlayers = 2;
            var lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers);

            hostLobby = lobby;

            Debug.Log("Created lobby: " + lobby.Name + " max players " + lobby.MaxPlayers);
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

    }

}
