using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.Assertions.Must;
using System;

public class CrossZeroRelay : MonoBehaviour
{
    private const int MAX_PLAYERS = 2;

    private static RelayServerData _serverData;

    private static event Action OnRelayConnected;

    private void Start()
    {
        OnRelayConnected += StartClient;
    }

    public async Task<string> CreateRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(MAX_PLAYERS);

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");

            _serverData = relayServerData;

            UIManager.GUIMessage("Start relay with code: " + joinCode);

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartHost();

            return joinCode;
        }
        catch (RelayServiceException e)
        {
            UIManager.GUIMessage(e.ToString());
            return null;
        }
    }

    public async Task JoinRelay(string relayCode)
    {
        try
        {
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(relayCode);

            RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");

            //NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            //NetworkManager.Singleton.StartClient();
        }
        catch (RelayServiceException e)
        {
            UIManager.GUIMessage(e.ToString());
        }

        if (JoinRelay(relayCode).IsCompleted)
        {
            OnRelayConnected?.Invoke();
            UIManager.GUIMessage("SSSSSS");
        }
    }

    public static void StartClient()
    {
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(_serverData);

        NetworkManager.Singleton.StartClient();

        UIManager.GUIMessage("CLient start");
    }
}
