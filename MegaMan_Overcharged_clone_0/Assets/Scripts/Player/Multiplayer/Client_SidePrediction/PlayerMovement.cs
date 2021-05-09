using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mirror;
using UnityEngine;

namespace Assets.Scripts.Player.Multiplayer.Client_SidePrediction
{
    public class PlayerMovement : NetworkBehaviour
    {
        public void MovePlayer(Rigidbody rigidbody, InputsStamp inputsStamp)
        {
            if (inputsStamp.Forward) rigidbody.AddForce(transform.forward * 50);
            if (inputsStamp.Backward) rigidbody.AddForce(-transform.forward * 50);
            if (inputsStamp.Left) rigidbody.AddForce(-transform.right * 50); ;
            if (inputsStamp.Right) rigidbody.AddForce(transform.right * 50);
            if (inputsStamp.Jump) rigidbody.AddForce(transform.up * 100);
        }
    }
}
