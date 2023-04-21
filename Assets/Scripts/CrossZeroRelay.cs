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
using System.Threading.Tasks;

public class CrossZeroRelay : MonoBehaviour
{
    private const int MAX_PLAYERS = 2;
    private const string CONNECTION_TYPE = "dtls";

    private static RelayServerData _serverData;

    public async Task<string> CreateRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(MAX_PLAYERS);

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            RelayServerData relayServerData = new RelayServerData(allocation, CONNECTION_TYPE);

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

            RelayServerData relayServerData = new RelayServerData(joinAllocation, CONNECTION_TYPE);

            _serverData = relayServerData;

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartClient();
        }
        catch (RelayServiceException e)
        {
            UIManager.GUIMessage(e.ToString());
        }

    }

    public static void StartClient()
    {
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(_serverData);

        NetworkManager.Singleton.StartClient();

        UIManager.GUIMessage("CLient start");
    }
}
