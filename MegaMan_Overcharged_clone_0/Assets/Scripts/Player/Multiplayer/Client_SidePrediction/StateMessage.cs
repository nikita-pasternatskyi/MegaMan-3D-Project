using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Multiplayer.Client_SidePrediction
{
    public class StateMessage
    {
        public Vector3 Position;
        public Vector3 AngularVelocity;
        public Vector3 Velocity;
        public Quaternion Rotation;

        public uint Tick;

        public StateMessage(Vector3 position, Quaternion rotation, Vector3 velocity, Vector3 angularVelocity, uint tick)
        {
            Position = position;
            Rotation = rotation;
            Velocity = velocity;
            AngularVelocity = angularVelocity;

            Tick = tick;
        }

        public StateMessage()
        {
            Position = Vector3.zero;
            Rotation = Quaternion.identity;
            Velocity = Vector3.zero;
            AngularVelocity = Vector3.zero;
            Tick = 0;
        }
    }
}
