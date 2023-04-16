using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class NetworkStorage : NetworkBehaviour //rename it
{
    public static event Action<string> OnShowMyName;

    public override void OnNetworkSpawn()
    {
        PlayerNameModel.SayMyName += IsServer ? TestClientRPC : TestServerRPC;
    }

    [ServerRpc(RequireOwnership = false)] // This code is runnig on server when client is acting
    private void TestServerRPC(string name) //Rename !!!
    {
        OnShowMyName?.Invoke(name);
    }

    [ClientRpc] // This code is runnig on client when server is acting
    private void TestClientRPC(string name) //Rename !!!
    {
        if (IsHost) return;
        OnShowMyName?.Invoke(name);
    }
}
