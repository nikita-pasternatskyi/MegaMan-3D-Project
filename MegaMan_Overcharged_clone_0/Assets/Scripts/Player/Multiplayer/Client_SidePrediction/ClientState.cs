using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Multiplayer.Client_SidePrediction
{
    public class ClientState
    {
        public uint Tick;
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Velocity;

        public ClientState(uint tick, Vector3 position, Quaternion rotation, Vector3 velocity)
        {
            Tick = tick;
            Position = position;
            Rotation = rotation;
            Velocity = velocity;
        }

        public ClientState()
        {
            Tick = 0;
            Position = Vector3.zero;
            Velocity = Vector3.zero;
        }
    }
}
