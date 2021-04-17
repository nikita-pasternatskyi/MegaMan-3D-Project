using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
    [AddComponentMenu("Player/Base/Movement")]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMove : NetworkBehaviour
    {
        [Header("Movement Parameters")]
        [SerializeField] private Transform _forwardDirectionReference;
        [SerializeField] private float _speed;
        [SerializeField] private float _sprintSpeedMultiplier;
        [SerializeField] private float _gravity;
        [SerializeField] private float _jumpHeight;
        private Vector3 _velocity;
        private Vector2 _currentInput;

        [Header("Class dependencies")]
        [SerializeField] private CharacterController _characterController;

        [Header("Physics Check")]
        [SerializeField] private float _groundDistance;
        [SerializeField] private LayerMask _whatIsGround;
        private bool _isGrounded;

        [Header("Client-Side Prediction")]
        [SyncVar(hook = "OnServerStateChanged")]
        public PlayerTransformState _state;

        [SerializeField] private float PlayerFixedUpdateInterval;
        [SerializeField] private float PlayerLerpSpacing;
        [SerializeField] private float PlayerLerpEasing;

        private PlayerTransformState _predictedState;
        private List<CollectedPlayerInput> _pendingInputs;

        private void Awake() => InitState();
        private void FixedUpdate()
        {        
            if (isLocalPlayer)
            {
                ApplyPhysics();
                ApplyVelocity();              
            }
            SyncState();
        }
        public override void OnStartLocalPlayer() => _pendingInputs = new List<CollectedPlayerInput>();

        [ClientCallback]
        private void OnMove(InputValue value) => _currentInput = value.Get<Vector2>();

        [Command]
        private void OnJump()
        {
            _velocity.y = _isGrounded ? Mathf.Sqrt(_jumpHeight * -2 * _gravity) : _velocity.y;
        }

        [Command]
        private void ApplyPhysics()
        {
            Vector3 groundCheckPosition = new Vector3
                (_characterController.bounds.center.x,
                _characterController.bounds.center.y - _characterController.height / 2,
                _characterController.bounds.center.z);

            _isGrounded = Physics.CheckSphere(groundCheckPosition, _groundDistance, _whatIsGround);

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }
            if (!_isGrounded && _velocity.y > -50)
            {
                _velocity.y += _gravity * Time.fixedDeltaTime;
            }
            else if (!_isGrounded && _velocity.y < -50)
            {
                return;
            }
        }

        #region Client_Side_Prediction
        private void InitState()
        {
            _state = new PlayerTransformState
            {
                TimeStamp = 0,
                Position = transform.position,
            };
        }
        [Command]
        private void CmdMoveOnServer(CollectedPlayerInput playerInput) => _state = MovePlayer(_state, playerInput);
        private PlayerTransformState MovePlayer(PlayerTransformState playerTransformState, CollectedPlayerInput playerInput)
        {
            CalculateVelocity(playerInput.Direction);
            Vector3 newPosition = playerTransformState.Position;
            if (!_velocity.Equals(new Vector3(0,-2f,0)))
            {
                if (isServer)
                {
                    if (isLocalPlayer)
                        _characterController.Move(_velocity * PlayerFixedUpdateInterval / 2);
                    else
                    {
                        _characterController.Move(_velocity * PlayerFixedUpdateInterval);
                    }
                    newPosition = transform.position;
                }
                else if (isClient)
                {
                    newPosition = playerTransformState.Position += _velocity * PlayerFixedUpdateInterval;
                }
            }
            return new PlayerTransformState
            {
                TimeStamp = playerTransformState.TimeStamp + 1,
                Position = newPosition,
            };
        }
        private void ApplyVelocity()
        {
            CollectedPlayerInput playerInput = CreatePlayerInput();
            if (playerInput != null)
            {
                _pendingInputs.Add(playerInput);
                UpdatePredictedState();
                CmdMoveOnServer(playerInput);
            };
        }
        private void CalculateVelocity(Vector2 input)
        {
            var direction = _forwardDirectionReference.forward * input.y + _forwardDirectionReference.right * input.x;
            _velocity = new Vector3(direction.x * _speed, _velocity.y, direction.z * _speed);
        }
        private CollectedPlayerInput CreatePlayerInput()
        {
            CollectedPlayerInput playerInput = new CollectedPlayerInput();
            playerInput.Direction = _currentInput;
            //if (playerInput.Direction == Vector2.zero)
            //    return null;
            return playerInput;
        }
        private void UpdatePredictedState()
        {
            _predictedState = _state;

            foreach (CollectedPlayerInput playerInput in _pendingInputs)
            {
                _predictedState = MovePlayer(_predictedState, playerInput);
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
                while(_pendingInputs.Count > _predictedState.TimeStamp - _state.TimeStamp)
                {
                    _pendingInputs.RemoveAt(0);
                }
                UpdatePredictedState();
            }
        }
        #endregion


        //private void FixedUpdate()
        //{
        //    if (isLocalPlayer)
        //    {
        //        ApplyPhysics();
        //    }
        //}


        //[Command]
        //private void ApplyPhysics()
        //{
        //    Vector3 groundCheckPosition = new Vector3
        //        (_characterController.bounds.center.x,
        //        _characterController.bounds.center.y - _characterController.height / 2,
        //        _characterController.bounds.center.z);

        //    _isGrounded = Physics.CheckSphere(groundCheckPosition, _groundDistance, _whatIsGround);

        //    if (_isGrounded && _velocity.y < 0)
        //    {
        //        _velocity.y = -2f;
        //    }
        //    else if (!_isGrounded)
        //    {
        //        _velocity.y += _gravity * Time.fixedDeltaTime;
        //    }
        //}
    }

    //void OnServerStateChanged(Assets.Scripts.Player.PlayerTransformState oldValue, Assets.Scripts.Player.PlayerTransformState newValue
}
