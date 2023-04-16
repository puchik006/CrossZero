using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkInterface : MonoBehaviour
{
    private NetworkManager _netManager;

    private void Start()
    {
        _netManager = GetComponent<NetworkManager>();
    }

    public void StartHost()
    {
        _netManager.StartHost();
    }

    public void StartCLient()
    {
        _netManager.StartClient();
    }
}
