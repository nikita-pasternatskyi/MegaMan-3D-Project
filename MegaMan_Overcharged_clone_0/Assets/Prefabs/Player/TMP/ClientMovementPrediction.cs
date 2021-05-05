using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Prefabs.Player.TMP
{
    public class ClientMovementPrediction : NetworkBehaviour
    {
        [SyncVar(hook = "OnServerStateChanged")]
        public PlayerTransformState State;
        [SerializeField] private PlayerPhysics _playerPhysics;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float PlayerFixedUpdateInterval;
        [SerializeField] private float PlayerLerpSpacing;
        [SerializeField] private float PlayerLerpEasing;
        private PlayerTransformState _predictedState;
        private List<MovementInputStamp> _pendingMovementInputs;
        private Vector3 velocity;
        private Vector2 _currentMovementInput;
        private Vector2 _currentMouseInput;
        private bool _currentJumpState;


        private void Awake()
        {
            InitializeState();
        }
        private void Start()
        {
            if (isLocalPlayer)
            {
                _pendingMovementInputs = new List<MovementInputStamp>();
            }
        }
        private void FixedUpdate()
        {
            if (isLocalPlayer)
            {
                MovementInputStamp inputStamp = CreateInputStamp();
                if (inputStamp != null)
                {
                    _pendingMovementInputs.Add(inputStamp);
                    UpdatePredictedState();
                    CmdMoveOnServer(inputStamp);
                }
            }

            SyncState();
        }
        private void InitializeState()
        {
            State = new PlayerTransformState
            {
                TimeStamp = 0,
                Position = transform.position,
                Rotation = transform.rotation,
            };
        }
        public void UpdatePredictedState()
        {
            _predictedState = State;

            foreach (MovementInputStamp movementInputStamp in _pendingMovementInputs)
            {
                _predictedState = PredictMovement(_predictedState, movementInputStamp);
            }
        }
        private void SyncState()
        {
            if (isServer)
            {
                transform.position = State.Position;
                transform.rotation = State.Rotation;
                return;
            }

            PlayerTransformState stateToShow = isLocalPlayer ? _predictedState : State;
            transform.position = Vector3.Lerp(transform.position, stateToShow.Position * PlayerLerpSpacing, PlayerLerpEasing);
        }

        [Command]
        private void CmdMoveOnServer(MovementInputStamp inputStamp)
        {
            State = PredictMovement(State, inputStamp);
        }
        private void CalculateVelocity(Vector2 movementInput, bool jumped)
        {
            velocity = new Vector3(movementInput.x * _speed, 0, movementInput.y * _speed);
            //velocity.y = jumped ? _jumpHeight : 0;
        }
        private PlayerTransformState PredictMovement(PlayerTransformState playerTransformState, MovementInputStamp movementInputStamp)
        {
            Vector3 newPosition = playerTransformState.Position;
            Quaternion newRotation = playerTransformState.Rotation;
            CalculateVelocity(movementInputStamp.MovementInput, movementInputStamp.JumpState);
            //newPosition = playerTransformState.Position + velocity * Time.fixedDeltaTime;
            if (isServer)
            {
                _characterController.Move(velocity * Time.fixedDeltaTime);
                //newPosition = playerTransformState.Position += velocity * Time.fixedDeltaTime;
                newPosition = transform.position;
            }
            else
            {
                if (movementInputStamp.JumpState)
                    velocity.y = _jumpHeight;

                newPosition = playerTransformState.Position + velocity * Time.fixedDeltaTime;

            }
            _currentJumpState = false;

            return new PlayerTransformState
            {
                Position = newPosition,
                TimeStamp = playerTransformState.TimeStamp + 1
            };

        }
        private MovementInputStamp CreateInputStamp()
        {
            MovementInputStamp movementInputStamp = new MovementInputStamp();
            movementInputStamp.MovementInput = _currentMovementInput;
            movementInputStamp.CameraLookDirection = _currentMouseInput;
            movementInputStamp.JumpState = _currentJumpState;

            if (movementInputStamp.MovementInput == Vector2.zero && movementInputStamp.CameraLookDirection == Vector2.zero && movementInputStamp.JumpState == false)
            {
                return null;
            }
            else
                return movementInputStamp;
        }


        public void OnServerStateChanged(PlayerTransformState oldState, PlayerTransformState newState)
        {
            State = newState;
            if (_pendingMovementInputs != null)
            {
                while (_pendingMovementInputs.Count > _predictedState.TimeStamp - State.TimeStamp)
                {
                    _pendingMovementInputs.RemoveAt(0);
                }
                UpdatePredictedState();
            }
        }
        private void OnMove(InputValue value)
        {
            _currentMovementInput = value.Get<Vector2>();
        }
        private void OnLook(InputValue value)
        {
            _currentMouseInput = value.Get<Vector2>();
        }
        private void OnJump()
        {
            if (_playerPhysics.IsGrounded)
            {
                //Debug.Log("make it true!");
                _currentJumpState = true;
                _playerPhysics.AddVelocity(_jumpHeight * transform.up);
            }
        }

        

    } 
}








