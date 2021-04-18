using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mirror;
using UnityEngine;

namespace Assets.Scripts.Player.Multiplayer
{
    class ClientSidePrediction : NetworkBehaviour
    {
        [SyncVar(hook = "OnServerStateChanged")]
        public PlayerTransformState _state;

        private Vector3 _velocity;
        public Vector3 Velocity { get => _velocity; set => _velocity += value; }

        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float PlayerFixedUpdateInterval;
        [SerializeField] private float PlayerLerpSpacing;
        [SerializeField] private float PlayerLerpEasing;

        private PlayerTransformState _predictedState;
        private List<CollectedPlayerInput> _pendingInputs;

        private void Awake() => InitState();
        private void InitState()
        {
            _state = new PlayerTransformState
            {
                TimeStamp = 0,
                Position = transform.position,
            };
        }
        public override void OnStartLocalPlayer() => _pendingInputs = new List<CollectedPlayerInput>();

        private void FixedUpdate()
        {
            SyncState();
        }

        private PlayerTransformState ProcessInput(PlayerTransformState state, CollectedPlayerInput playerInput)
        {



            return new PlayerTransformState
            {
                Position = Vector3.zero,
                TimeStamp = 1,
            };   
        }
        private void UpdatePredictedState()
        {
            _predictedState = _state;

            foreach (CollectedPlayerInput playerInput in _pendingInputs)
            {
                _predictedState = ProcessInput(_predictedState, playerInput);
            }
        }
        private void SyncState()
        {
            if (isServer)
            {
                transform.position = _state.Position;
                return;
            }

            PlayerTransformState stateToShow = isLocalPlayer ? _predictedState : _state;
            transform.position = Vector3.Lerp(transform.position, stateToShow.Position * PlayerLerpSpacing, PlayerLerpEasing);
        }
        public void OnServerStateChanged(PlayerTransformState oldState, PlayerTransformState newState)
        {
            _state = newState;
            if (_pendingInputs != null)
            {
                while (_pendingInputs.Count > _predictedState.TimeStamp - _state.TimeStamp)
                {
                    _pendingInputs.RemoveAt(0);
                }
                UpdatePredictedState();
            }
        }

    }
}





