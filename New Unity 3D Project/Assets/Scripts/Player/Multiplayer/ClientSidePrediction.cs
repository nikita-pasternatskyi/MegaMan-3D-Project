using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mirror;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class ClientSidePrediction : NetworkBehaviour
    {
        [SyncVar(hook = "OnServerStateChanged")]
        public PlayerTransformState State;

        [SyncVar] private Vector3 _velocity;
        public Vector3 Velocity { get => _velocity; set => _velocity += value; }

        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float PlayerFixedUpdateInterval;
        [SerializeField] private float PlayerLerpSpacing;
        [SerializeField] private float PlayerLerpEasing;

        [SyncVar] private Vector3 inputVelocity;

        private PlayerTransformState _predictedState;
        [SyncVar] private List<Vector3> _pendingVelocities;
        public void ReceiveVelocity(Vector3 calculatedVelocity)
        {
            _pendingVelocities.Add(calculatedVelocity);
            inputVelocity = calculatedVelocity;
            UpdatePredictedState();
            CmdMoveOnServer(calculatedVelocity);
        }

        public void AddVelocity(Vector3 velocityToAdd)
        {

            if (_pendingVelocities.Count == 0)
            {
                _pendingVelocities.Add(velocityToAdd);
            }
            else if (_pendingVelocities.Count > 0)
            {
                if (_pendingVelocities[_pendingVelocities.Count-1] != null)
                {
                    _pendingVelocities[_pendingVelocities.Count-1] += velocityToAdd;
                }
                else if (_pendingVelocities[_pendingVelocities.Count-1] == null)
                {
                    _pendingVelocities.Add(velocityToAdd);
                }
            }
        }

        private void Awake() => InitState();
        private void InitState()
        {
            State = new PlayerTransformState
            {
                TimeStamp = 0,
                Position = transform.position,
            };
        }
        public override void OnStartLocalPlayer() => _pendingVelocities = new List<Vector3>();
        private void FixedUpdate()
        {
            SyncState();
        }
        private PlayerTransformState PredictMovement(PlayerTransformState state, Vector3 velocity)
        {
            Vector3 newPosition = state.Position;
            if (!_velocity.Equals(new Vector3(0, -2f, 0)))
            {
                if (isServer)
                {
                    if (isLocalPlayer)
                        _characterController.Move(velocity * PlayerFixedUpdateInterval / 2);
                    else
                    {
                        _characterController.Move(velocity * PlayerFixedUpdateInterval);
                    }
                    newPosition = transform.position;
                }
                else if (isClient)
                {
                    newPosition = state.Position += velocity * PlayerFixedUpdateInterval;
                }
            }
            return new PlayerTransformState
            {
                TimeStamp = state.TimeStamp + 1,
                Position = newPosition,
            };
        }
        public void OnServerStateChanged(PlayerTransformState oldState, PlayerTransformState newState)
        {
            State = newState;
            if (_pendingVelocities != null)
            {
                while (_pendingVelocities.Count > _predictedState.TimeStamp - State.TimeStamp)
                {
                    _pendingVelocities.RemoveAt(0);
                }
                UpdatePredictedState();
            }
        }
        [Command]
        private void CmdMoveOnServer(Vector3 velocity) => State = PredictMovement(State, velocity);
        private void UpdatePredictedState()
        {
            _predictedState = State;

            foreach (Vector3 calculatedVelocity in _pendingVelocities)
            {
                _predictedState = PredictMovement(_predictedState, calculatedVelocity);
            }
        }
        private void SyncState()
        {
            if (isServer)
            {
                transform.position = State.Position;
                return;
            }

            PlayerTransformState stateToShow = isLocalPlayer ? _predictedState : State;
            transform.position = Vector3.Lerp(transform.position, stateToShow.Position * PlayerLerpSpacing, PlayerLerpEasing);
        }

    }
}








