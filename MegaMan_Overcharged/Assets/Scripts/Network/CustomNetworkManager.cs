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
        public static event Action<NetworkConnection> OnServerDisconnected;
        public override void OnServerReady(NetworkConnection connection)
        {
            base.OnServerReady(connection);
            OnServerReadied?.Invoke(connection);
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            base.OnServerDisconnect(conn);
            OnServerDisconnected?.Invoke(conn);
        }
    }
}
