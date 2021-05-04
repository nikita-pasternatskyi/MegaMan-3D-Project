using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Mirror;
using UnityEngine;

namespace Assets.Prefabs.Player.TMP
{
    public class ClientSidePrediction : NetworkBehaviour
    {
        //[SyncVar(hook = "OnServerStateChanged")]
        public PlayerTransformState State;

        [SerializeField] private PlayerPhysics _playerPhysics;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float PlayerFixedUpdateInterval;
        [SerializeField] private float PlayerLerpSpacing;
        [SerializeField] private float PlayerLerpEasing;

        private PlayerTransformState _predictedState;
        private List<InputStamp> _pendingInputStamps;
        

        //public void ReceiveInputs(List<InputStamp> inputStamps)
        //{
        //    foreach (var item in inputStamps)
        //    {
        //        _pendingInputStamps.Add(item);
        //    }
        //    UpdatePredictedState();
        //    //CmdMoveOnServer();
        //}

        public void ReceiveVelocity(Vector3 velocityToReceive)
        {
            _characterController.Move(velocityToReceive);
        }



        //private void UpdatePredictedState()
        //{
        //    _predictedState = State;

        //    foreach (InputStamp inputStamp in _pendingInputStamps)
        //    {
        //         _predictedState = ExecuteInput(_predictedState, inputStamp);
        //    }
        //}

        //private PlayerTransformState ExecuteInput(PlayerTransformState playerTransformState, InputStamp inputToExecute)
        //{
        //    Vector3 newPosition = playerTransformState.Position;

        //    return new PlayerTransformState
        //    {
        //        TimeStamp = playerTransformState.TimeStamp + 1,
        //        Position = newPosition,
        //    };
        //}

        //public void OnServerStateChanged(PlayerTransformState oldState, PlayerTransformState newState)
        //{
        //    State = newState;
        //    if (_pendingVelocities != null)
        //    {
        //        while (_pendingVelocities.Count > _predictedState.TimeStamp - State.TimeStamp)
        //        {
        //            _pendingVelocities.RemoveAt(0);
        //        }
        //        UpdatePredictedState();
        //    }
        //}

        //public void ReceiveVelocity(Vector3 velocityToReceive)
        //{
        //    _pendingVelocities.Add(velocityToReceive);
        //    UpdatePredictedState();
        //    CmdMoveOnServer(velocityToReceive);
        //}

        //private void Awake() => InitState();
        //private void InitState()
        //{
        //    State = new PlayerTransformState
        //    {
        //        TimeStamp = 0,
        //        Position = transform.position,
        //    };
        //}
        //public override void OnStartAuthority()
        //{
        //    _pendingVelocities = new List<Vector3>();
        //}
        //private void FixedUpdate()
        //{
        //    SyncState();
        //}
        //public void OnServerStateChanged(PlayerTransformState oldState, PlayerTransformState newState)
        //{
        //    State = newState;
        //    if (_pendingVelocities != null)
        //    {
        //        while (_pendingVelocities.Count > _predictedState.TimeStamp - State.TimeStamp)
        //        {
        //            _pendingVelocities.RemoveAt(0);
        //        }
        //        UpdatePredictedState();
        //    }
        //}

        //[Command]
        //private void CmdMoveOnServer(Vector3 velocity) => State = PredictMovement(State, velocity);
        //private PlayerTransformState PredictMovement(PlayerTransformState state, Vector3 velocity)
        //{
        //    Vector3 newPosition = state.Position;
        //    if (isServer)
        //    {
        //        _characterController.Move(velocity * Time.fixedDeltaTime);

        //        newPosition = transform.position;
        //    }
        //    else if (isClient)
        //    {
        //        if (velocity.y < 0)
        //        {
        //            velocity.y = State.Position.y;
        //        }
        //        newPosition = state.Position + velocity * Time.fixedDeltaTime;
        //    }
        //    return new PlayerTransformState
        //    {
        //        TimeStamp = state.TimeStamp + 1,
        //        Position = newPosition,
        //    };
        //}

        //private void UpdatePredictedState()
        //{
        //    _predictedState = State;

        //    foreach (Vector3 calculatedVelocity in _pendingVelocities)
        //    {
        //        _predictedState = PredictMovement(_predictedState, calculatedVelocity);
        //    }
        //}
        //private void SyncState()
        //{
        //    if (isServer)
        //    {
        //        transform.position = State.Position;
        //        return;
        //    }

        //    PlayerTransformState stateToShow = isLocalPlayer ? _predictedState : State;
        //    transform.position = Vector3.Lerp(transform.position, stateToShow.Position * PlayerLerpSpacing, PlayerLerpEasing);
        //}
    }
}








