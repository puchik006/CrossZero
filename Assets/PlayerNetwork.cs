using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    public static event Action<int> OnGetClientFieldInfo;

    public override void OnNetworkSpawn()
    {
        FieldView.OnButtonClick += IsServer? TestClientRPC : TestServerRPC;
    }

    [ServerRpc(RequireOwnership = false)]
    private void TestServerRPC(int fieldNumber)
    {
        if (IsOwner) return;
        OnGetClientFieldInfo?.Invoke(fieldNumber);
    }

    [ClientRpc]
    private void TestClientRPC(int fieldNumber)
    {
        if (IsOwner) return;
        if (IsHost) return;
        OnGetClientFieldInfo?.Invoke(fieldNumber);
    }
}
