using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    public static event Action<int> OnGetClientFieldInfo;
    public static event Action<bool> IsPlayerHost;

    public override void OnNetworkSpawn()
    {
        FieldView.OnButtonClick += IsServer? TestClientRPC : TestServerRPC;
        IsPlayerHost?.Invoke(IsServer ? true : false);
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
