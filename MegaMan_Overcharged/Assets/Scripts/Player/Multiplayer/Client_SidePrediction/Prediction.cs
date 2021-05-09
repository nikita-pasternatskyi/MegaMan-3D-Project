using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Assets.Scripts.Player.Multiplayer.Client_SidePrediction
{
    public class Prediction : NetworkBehaviour
    {
        public PlayerMovement _playerMovementClient;
        public Rigidbody _playerRigidbody;

        private uint _tickNumber;

        private ClientState[] _clientStateBuffer = new ClientState[1024];
        private InputsStamp[] _clientInputsStampBuffer = new InputsStamp[1024];
        private InputsStamp _currentInputsStamp;

        [ClientCallback]
        private void Awake()
        {
            for (int i = 0; i < _clientStateBuffer.Length; i++)
            {
                _clientStateBuffer[i] = new ClientState();
            }
        }

        [ClientCallback]
        private void Update()
        {
            if (isLocalPlayer)
            {
                _currentInputsStamp = SetInputs();

                InputMessage inputMessage = new InputMessage();

                inputMessage.InputsStamp = _currentInputsStamp;

                uint bufferSlot = this._tickNumber % 1024;
                _clientInputsStampBuffer[bufferSlot] = _currentInputsStamp;
                _clientStateBuffer[bufferSlot].Position = _playerMovementClient.GetComponent<Transform>().position;
                _clientStateBuffer[bufferSlot].Rotation = _playerMovementClient.GetComponent<Transform>().rotation;

                
                SendToServer(inputMessage);

                _playerMovementClient.MovePlayer(_playerRigidbody, _currentInputsStamp);
                Physics.Simulate(Time.fixedDeltaTime);
                ++_tickNumber;
            }
        }


        [ClientCallback]
        public InputsStamp SetInputs()
        {
            InputsStamp inputsStamp = new InputsStamp();
            inputsStamp.Forward = Keyboard.current.wKey.isPressed;
            inputsStamp.Backward = Keyboard.current.sKey.isPressed;
            inputsStamp.Left = Keyboard.current.aKey.isPressed;
            inputsStamp.Right = Keyboard.current.dKey.isPressed;
            inputsStamp.Jump = Keyboard.current.spaceKey.isPressed;

            return inputsStamp;
        }

        [TargetRpc]
        public void SendToClient(NetworkConnection target, StateMessage stateMessage)
        {
            uint bufferSlot = stateMessage.Tick % 1024;
            Vector3 positionError = stateMessage.Position - this._clientStateBuffer[bufferSlot].Position;

            if (positionError.sqrMagnitude > 0.0000001f)
            {
                _playerRigidbody.position = stateMessage.Position;
                _playerRigidbody.rotation = stateMessage.Rotation;
                _playerRigidbody.angularVelocity = stateMessage.AngularVelocity;
                _playerRigidbody.velocity = stateMessage.Velocity;

                uint rewindTickNumber = stateMessage.Tick;
                while (rewindTickNumber < this._tickNumber)
                {
                    bufferSlot = rewindTickNumber % 1024;
                    this._clientInputsStampBuffer[bufferSlot] = _currentInputsStamp;
                    this._clientStateBuffer[bufferSlot].Position = _playerRigidbody.position;
                    this._clientStateBuffer[bufferSlot].Rotation = _playerRigidbody.rotation;
                    
                    _playerMovementClient.MovePlayer(_playerRigidbody, _currentInputsStamp);
                    Physics.Simulate(Time.fixedDeltaTime);

                    ++rewindTickNumber;
                }
            }
        }

        [Command]
        public void SendToServer(InputMessage inputMessage)
        {
             StateMessage stateMessage = new StateMessage();
            _playerMovementClient.MovePlayer(_playerRigidbody, inputMessage.InputsStamp);
            Physics.Simulate(Time.fixedDeltaTime);

           
            stateMessage.Position = _playerRigidbody.position;
            stateMessage.Rotation = _playerRigidbody.rotation;
            stateMessage.Velocity = _playerRigidbody.velocity;
            stateMessage.AngularVelocity = _playerRigidbody.angularVelocity;

            stateMessage.Tick = inputMessage.Tick++;

            SendToClient(connectionToClient, stateMessage);
        }
    }
}
