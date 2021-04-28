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

        public PlayerPhysics plPh;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float PlayerFixedUpdateInterval;
        [SerializeField] private float PlayerLerpSpacing;
        [SerializeField] private float PlayerLerpEasing;

        private PlayerTransformState _predictedState;
        private List<Vector3> _pendingVelocities;

        public void ReceiveVelocity(Vector3 velocityToReceive)
        {
            _pendingVelocities.Add(velocityToReceive);
            UpdatePredictedState();
            CmdMoveOnServer(velocityToReceive);    
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
        public override void OnStartAuthority()
        {
            _pendingVelocities = new List<Vector3>();
        }
        private void FixedUpdate()
        {
            SyncState();
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
        private PlayerTransformState PredictMovement(PlayerTransformState state, Vector3 velocity)
        {
            Vector3 newPosition = state.Position;
            if (isServer)
                {
                    if (isLocalPlayer)
                        _characterController.Move(velocity * PlayerFixedUpdateInterval / 2f);
                    else
                    {
                        _characterController.Move(velocity * PlayerFixedUpdateInterval);
                    }
                    newPosition = transform.position;
                }
                else if (isClient)
                {
                    if (velocity.y < 0)
                    {
                        velocity.y = State.Position.y;
                    }
                newPosition = state.Position + velocity * PlayerFixedUpdateInterval;
                }
            return new PlayerTransformState
            {
                TimeStamp = state.TimeStamp + 1,
                Position = newPosition,
            };
        }

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
            //transform.position = State.Position;
        }
    }
}








