using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

namespace Assets.Scripts.Network
{
    public class CustomNetworkManager : NetworkManager
    {
        public static event Action<NetworkConnection> OnServerReadied;

        public override void OnServerReady(NetworkConnection connection)
        {
            base.OnServerReady(connection);
            OnServerReadied?.Invoke(connection);
        }
    }
}
