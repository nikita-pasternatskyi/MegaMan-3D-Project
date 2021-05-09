using System;
using System.Collections;
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
        private PlayerTransformState _serverCurrentState;
        private PlayerTransformState _clientPredictionState;
        [SyncVar] private Vector3 _serverPosition;

        private List<MovementInputStamp> _pendingMovementInputs;
        [SerializeField] private float PlayerLerpSpacing;
        [SerializeField] private float PlayerLerpEasing;

        private Vector3 _clientVelocity;
        [SyncVar] private Vector3 _serverVelocity;

        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private PlayerClassConfiguration _currentClassConfiguration;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _groundCheckRadius;
        [SyncVar] private bool _isGrounded;

        private bool _currentJumpState;
        private Vector2 _currentMovementInput;
        private Vector2 _currentMouseInput;

        private void Awake()
        {
            InitializeState();
        }
        private void Start()
        {
            if (isLocalPlayer)
            {
                StartCoroutine(CheckState());
                _pendingMovementInputs = new List<MovementInputStamp>();
            }
        }

        private void InitializeState()
        {
            _serverCurrentState = new PlayerTransformState
            {
                TimeStamp = 0,
                Position = transform.position,
                Rotation = transform.rotation,
                Velocity = Vector3.zero,
            };
        }
        public void UpdatePredictedState()
        {
            _clientPredictionState = _serverCurrentState;

            foreach (MovementInputStamp movementInputStamp in _pendingMovementInputs)
            {
                _clientPredictionState = PredictMovement(_clientPredictionState, movementInputStamp);
            }
        }
        private void SyncState()
        {
            if (isServer)
            {
                transform.position = _serverCurrentState.Position;
                transform.rotation = _serverCurrentState.Rotation;
                return;
            }

            PlayerTransformState stateToShow = isLocalPlayer ? _clientPredictionState : _serverCurrentState;
            transform.position = Vector3.Lerp(transform.position, stateToShow.Position * PlayerLerpSpacing, PlayerLerpEasing);
        }

        [Command]
        private void CacheServerPosition()
        {
            _serverPosition = transform.position;
        }

        [Command]
        private void CmdMoveOnServer(MovementInputStamp inputStamp)
        {
            _serverCurrentState = PredictMovement(_serverCurrentState, inputStamp);
        }
        private void FixedUpdate()
        {
            if (isLocalPlayer)
            {
                PhysicsStep();
                PhysicsStepClient();
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


        [Command]
        private void PhysicsStep()
        {
            CheckGround();
            _serverVelocity.y += _currentClassConfiguration.Gravity * Time.fixedDeltaTime * _currentClassConfiguration.Mass;
            if (_serverVelocity.y < 0 && _isGrounded)
            {
                _serverVelocity.y = 0;
            }
            _characterController.Move(_serverVelocity * Time.fixedDeltaTime);
            _serverCurrentState.Position = transform.position;
        }
        private void PhysicsStepClient()
        {
            CheckGround();
            _clientVelocity.y += _currentClassConfiguration.Gravity * Time.fixedDeltaTime * _currentClassConfiguration.Mass;
            if (_clientVelocity.y < 0 && _isGrounded)
            {
                _clientPredictionState.Position.y = _serverCurrentState.Position.y;
                _clientVelocity.y = 0;
            }
            else
            {
                _clientPredictionState.Position += _clientVelocity * Time.fixedDeltaTime;
            }
        }
        private void ResyncStates()
        {
            CacheServerPosition();
            _clientPredictionState.Position = _serverPosition;
            _clientPredictionState.Velocity = Vector3.zero;
            transform.position = _serverPosition;
        }


        private void CalculateVelocity(Vector2 movementInput, bool jumped, ref Vector3 velocityToOutput, ref Vector3 referenceVelocity)
        {
            velocityToOutput = new Vector3(movementInput.x * _speed, referenceVelocity.y, movementInput.y * _speed);
        }

        private PlayerTransformState PredictMovement(PlayerTransformState playerTransformState, MovementInputStamp movementInputStamp)
        {
            Vector3 newPosition = playerTransformState.Position;
            Quaternion newRotation = playerTransformState.Rotation;
            
            if (isServer)
            {
                CalculateVelocity(movementInputStamp.MovementInput, movementInputStamp.JumpState, ref _serverVelocity, ref _serverVelocity);
                if (movementInputStamp.JumpState)
                {
                    _serverVelocity.y += _jumpHeight * Time.fixedDeltaTime;
                }
                _characterController.Move(_serverVelocity * Time.fixedDeltaTime);
                newPosition = transform.position;
            }
            else if (isClient)
            {
                CalculateVelocity(movementInputStamp.MovementInput, movementInputStamp.JumpState, ref _clientVelocity, ref _clientVelocity);
                if (movementInputStamp.JumpState)
                {
                    _clientVelocity.y += _jumpHeight * Time.fixedDeltaTime;
                }
                newPosition = playerTransformState.Position + _clientVelocity * Time.fixedDeltaTime;

            }
            _currentJumpState = false;
            if (isServer)
            {
                return new PlayerTransformState
                {
                    Position = newPosition,
                    TimeStamp = playerTransformState.TimeStamp + 1,
                    Velocity = _serverVelocity,
                };
            }
            else {
                return new PlayerTransformState
                {
                    Position = newPosition,
                    TimeStamp = playerTransformState.TimeStamp + 1,
                };
            }

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
        private IEnumerator CheckState()
        {
            yield return new WaitForSeconds(1/60f);
            Vector3 difference = transform.position - _serverPosition;
            ResyncStates();

            yield return CheckState();
        }
        public void OnServerStateChanged(PlayerTransformState oldState, PlayerTransformState newState)
        {
            _serverCurrentState = newState;
            if (_pendingMovementInputs != null)
            {
                while (_pendingMovementInputs.Count > _clientPredictionState.TimeStamp - _serverCurrentState.TimeStamp)
                {
                    _pendingMovementInputs.RemoveAt(0);
                }
                UpdatePredictedState();
            }
        }



        #region Input_Processing
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

            if (_isGrounded)
            {
                _currentJumpState = true;
                MovementInputStamp inputStamp = CreateInputStamp();
                _pendingMovementInputs.Add(inputStamp);
                UpdatePredictedState();
                CmdMoveOnServer(inputStamp);
            }
        }
        #endregion

        #region Physics
        //private void ApplyDrag(float drag, float delta)
        //{
        //    float multiplier = 1.0f - drag * delta;
        //    if (multiplier < 0.0f) multiplier = 0.0f;
        //    _physicsVelocity.x *= multiplier;
        //    _physicsVelocity.z *= multiplier;
        //}

        public void CheckGround()
        {
            Vector3 groundCheckPosition = new Vector3
                    (_characterController.bounds.center.x,
                    _characterController.bounds.center.y - _characterController.height / 2,
                    _characterController.bounds.center.z);

            _isGrounded = UnityEngine.Physics.CheckSphere(groundCheckPosition, _groundCheckRadius, _whatIsGround);
        }

        #endregion
    }
}



