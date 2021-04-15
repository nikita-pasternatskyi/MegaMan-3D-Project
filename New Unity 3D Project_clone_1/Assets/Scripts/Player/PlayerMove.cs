using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Mirror;

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

        [Header("Class dependencies")]
        [SerializeField] private PlayerCamera _playerCamera;
        [SerializeField] private CharacterController _characterController;

        [Header("Physics Check")]
        [SerializeField] private float _groundDistance;
        [SerializeField] private LayerMask _whatIsGround;
        private bool _isGrounded;

        public Transform CharacterControllerPredictionObject;

        [SyncVar(hook = "OnServerStateChanged")]
        public PlayerTransformState _state;

        public Vector3 delta;

        public float PlayerFixedUpdateInterval;
        public float PlayerLerpSpacing;
        public float PlayerLerpEasing;

        private PlayerTransformState _predictedState;
        private List<PlayerInput> _pendingInputs;

        private void Awake()
        {
            InitState();
        }

        private void InitState()
        {
            _state = new PlayerTransformState
            {
                TimeStamp = 0,
                Position = transform.position,
                Rotation = transform.rotation,
            };
        }

        private void Start()
        {
            if (isLocalPlayer)
            {
                _pendingInputs = new List<PlayerInput>();
            }
        }

        private void FixedUpdate()
        {
            if (isLocalPlayer)
            {
                PlayerInput playerInput = CreatePlayerInput();

                if (playerInput != null)
                {
                    _pendingInputs.Add(playerInput);
                    UpdatePredictedState();
                    CmdMoveOnServer(playerInput);
                }
            }
            SyncState();
        }

        private PlayerInput CreatePlayerInput()
        {
            PlayerInput playerInput = new PlayerInput();
            playerInput.Forward = UnityEngine.Input.GetAxis("Vertical");
            playerInput.Right = UnityEngine.Input.GetAxis("Horizontal");
            if (playerInput.Forward == 0 && playerInput.Right == 0)
                return null;
            return playerInput;
        }

        [Command]
        private void CmdMoveOnServer(PlayerInput playerInput)
        { 
            _state = UsePlayerInput(_state, playerInput);
        }

        private PlayerTransformState UsePlayerInput(PlayerTransformState playerTransformState, PlayerInput playerInput )
        {
            Vector3 newPosition = playerTransformState.Position;
            if (playerInput.Forward != 0 || playerInput.Right != 0)
            {
                if (isServer && !isLocalPlayer)
                {
                    Debug.Log("i to i e2222to");
                    Vector2 velocity = CalculateVelocity(new Vector2(playerInput.Forward, playerInput.Right));
                    Vector3 final = new Vector3(velocity.x, 0, velocity.y);
                    _characterController.Move(final * PlayerFixedUpdateInterval);
                    newPosition = transform.position;
                }
                else if(isServer && isLocalPlayer)
                {
                    Debug.Log("i to i eto");
                    Vector2 velocity = CalculateVelocity(new Vector2(playerInput.Forward, playerInput.Right));
                    Vector3 final = new Vector3(velocity.x, 0, velocity.y);
                    _characterController.Move(final * PlayerFixedUpdateInterval/2);
                    newPosition = transform.position;
                }
                else if(isClient)
                {
                    Vector2 velocity = CalculateVelocity(new Vector2(playerInput.Forward, playerInput.Right));
                    Vector3 final = new Vector3(velocity.x, 0, velocity.y);
                    newPosition = playerTransformState.Position += final * PlayerFixedUpdateInterval;
                }
            }
            return new PlayerTransformState
            {
                TimeStamp = playerTransformState.TimeStamp + 1,
                Position = newPosition,
            };
        }

        private void UpdatePredictedState()
        {
            _predictedState = _state;

            foreach (PlayerInput playerInput in _pendingInputs)
            {
                _predictedState = UsePlayerInput(_predictedState, playerInput);
            }
        }

        private Vector2 CalculateVelocity(Vector2 input)
        {
            var direction = _forwardDirectionReference.forward * input.y + _forwardDirectionReference.right * input.x;
            return new Vector2(direction.x * _speed, direction.z * _speed);
        }

        private void SyncState()
        {
            if (isServer)
            {
                delta = _predictedState.Position - _state.Position;
                transform.position = _state.Position;
                transform.rotation = _state.Rotation;
                return;
            }
            delta = _predictedState.Position - _state.Position;
            PlayerTransformState stateToShow = isLocalPlayer ? _predictedState : _state;

            transform.position = Vector3.Lerp(transform.position, stateToShow.Position * PlayerLerpSpacing, PlayerLerpEasing);
            ///transform.rotation = Quaternion.Lerp(transform.rotation, stateToShow.Rotation, PlayerLerpEasing);
        }

        public void OnServerStateChanged(PlayerTransformState oldState, PlayerTransformState newState)
        {
            delta = newState.Position - _state.Position;
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

        //public override void OnStartLocalPlayer()
        //{
        //    Input.MovementPressed += CalculateVelocity;
        //    Input.SprintHeld += Sprint;
        //    Input.JumpPressed += Jump;
        //}

        //private void FixedUpdate()
        //{
        //    if (isLocalPlayer)
        //    {
        //        ApplyPhysics();
        //        Move();
        //    }
        //}

        //public override void OnStopAuthority()
        //{
        //    Input.MovementPressed -= CalculateVelocity;
        //    Input.SprintHeld -= Sprint;
        //    Input.JumpPressed -= Jump;
        //}

        //[Command]
        //private void Jump()
        //{
        //    if (_isGrounded)
        //        _velocity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
        //}

        //public void Jump(float jumpHeight)
        //{
        //    _velocity.y = Mathf.Sqrt(jumpHeight * -2 * _gravity);
        //}

        //private void Sprint(bool Held)
        //{
        //    if (Held)
        //    {
        //        _speed *= _sprintSpeedMultiplier;
        //        //shakeSpeed *= sprintSpeedMultiplier;
        //    }
        //    else if (!Held)
        //    {
        //        _speed /= _sprintSpeedMultiplier;
        //        //playerCamera.shakeSpeed /= sprintSpeedMultiplier;
        //    }
        //}

        //[Command]
        //private void Move()
        //{
        //    _characterController.Move(_velocity * Time.fixedDeltaTime);
        //}

        //[Command]
        //private void CalculateVelocity(Vector2 input)
        //{
        //    var direction = _forwardDirectionReference.forward * input.y + _forwardDirectionReference.right * input.x;
        //    _velocity = new Vector3(direction.x * _speed, _velocity.y, direction.z * _speed);
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
