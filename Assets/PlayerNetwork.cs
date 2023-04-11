using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerNetwork : NetworkBehaviour
{
    public static event Action<int> OnGetClientFieldInfo;
    public static event Action<bool> IsPlayerHost;
    public static event Action<string,string> OnPlayerConnected;

    private NetworkVariable<FixedString32Bytes> _hostName = 
        new NetworkVariable<FixedString32Bytes>("",NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Server);
    private NetworkVariable<FixedString32Bytes> _clientName =
        new NetworkVariable<FixedString32Bytes>("", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public string HostName { get => _hostName.Value.ToString(); }
    public string ClientName { get => _clientName.Value.ToString(); }

    public override void OnNetworkSpawn()
    {
        FieldView.OnButtonClick += IsServer? TestClientRPC : TestServerRPC;
        IsPlayerHost?.Invoke(IsServer ? true : false);

        DefineNames();

        NetworkManager.OnClientConnectedCallback += IntroduceClientToHost;
    }


    private void IntroduceClientToHost(ulong asd)
    {
        OnPlayerConnected?.Invoke(HostName, ClientName);
        UIManager.GUIMessage("Client callback " + "\nHost name: " + _hostName.Value + "\nClient name: " + _clientName.Value);
    }

    private void DefineNames()
    {
        if (IsServer)
        {
            _hostName.Value = PlayerPrefs.HasKey(PlayerKeys.Name) ? PlayerPrefs.GetString(PlayerKeys.Name) : "Player 1";
        }
        else
        {
            _clientName.Value = PlayerPrefs.HasKey(PlayerKeys.Name) ? PlayerPrefs.GetString(PlayerKeys.Name) : "Player 2";
        }

        //OnPlayerConnected?.Invoke(HostName, ClientName);
    }

    [ServerRpc(RequireOwnership = false)] // This code is runnig on server when client is acting
    private void TestServerRPC(int fieldNumber) //Rename to SendInfoOfClickedButtonToClient
    {
        if (IsOwner) return;
        OnGetClientFieldInfo?.Invoke(fieldNumber);
    }

    [ClientRpc] // This code is runnig on client when server is acting
    private void TestClientRPC(int fieldNumber) //Rename to SendInfoOfClickedButtonToServer
    {
        if (IsOwner) return;
        if (IsHost) return;
        OnGetClientFieldInfo?.Invoke(fieldNumber);
    }
}
