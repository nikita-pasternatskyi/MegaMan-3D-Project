using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Prefabs.Player.TMP
{
    public class InputCollector : NetworkBehaviour
    {
        public List<InputStamp> _pendingInputStamps;
        public InputStamp _currentInputStamp;
        public InputHandler _inputHandler;
        public float _sendRate;
        
        private void Start()
        {
            if (isLocalPlayer)
            {
                _pendingInputStamps = new List<InputStamp>();
                StartCoroutine(SendInputPacketToServer());
            }
        }

        [ClientCallback]
        private IEnumerator SendInputPacketToServer()
        {
            yield return new WaitForSeconds(1/_sendRate);
            if(_pendingInputStamps!=null)
             _inputHandler.ReceieveInputPacketServer(_pendingInputStamps);
            yield return SendInputPacketToServer();
        }

        [ClientCallback]
        public void AddMethodCallAtThisFrame(Action<Vector2> delegateToAdd, Vector2 movementInput)
        {
            Debug.Log("Adding Method");
            _currentInputStamp = new InputStamp();
            _currentInputStamp.MethodToExecute = delegateToAdd;
            _currentInputStamp.MovementDirection = movementInput;
            _pendingInputStamps.Add(_currentInputStamp);
            SendInputPacketToPredict(_currentInputStamp);
        }

        [ClientCallback]
        private void SendInputPacketToPredict(InputStamp inputStamp)
        {
            _inputHandler.ReceieveInputPacketForPrediction(inputStamp);
        }
    }
}
