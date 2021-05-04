using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Prefabs.Player.TMP
{
    public class InputHandler : NetworkBehaviour
    {

        [Server]
        public void ReceieveInputPacketServer(List<InputStamp> inputStamps)
        {
            foreach (var item in inputStamps)
            {
                    ProcessInputServer(item);
            }
        }

        [Client]
        public void ReceieveInputPacketForPrediction(InputStamp inputStamp)
        {
            ProcessInputClient(inputStamp);
        }

        [Server]
        private void ProcessInputServer(InputStamp inputStamp)
        {
            ExecuteMethod<Vector2>(inputStamp.MethodToExecute, inputStamp.MovementDirection);
        }

        [Client]
        private void ProcessInputClient(InputStamp inputStamp)
        {
            ExecuteMethod<Vector2>(inputStamp.MethodToExecute, inputStamp.MovementDirection);
        }

        private void ExecuteMethod<T>(Action<Vector2> methodToExecute, Vector2 movementDirection)
        {
            methodToExecute(movementDirection);
        }

    }
}

