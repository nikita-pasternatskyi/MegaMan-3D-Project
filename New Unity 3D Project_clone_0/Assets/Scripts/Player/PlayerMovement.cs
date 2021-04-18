using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
    [AddComponentMenu("Player/Base/PlayerMovement")]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : NetworkBehaviour
    {
        [Header("Movement Parameters")]
        [SerializeField] private Transform _forwardDirectionReference;
        [SerializeField] private float _speed;
        [SerializeField] private float _sprintSpeedMultiplier;
        [SerializeField] private float _jumpHeight;
        private Vector3 _velocity;
        private Vector2 _currentInput;

        [Header("Class dependencies")]
        public ClientSidePrediction clientSidePrediction;

        private void Awake() => InitState();

        private void FixedUpdate()
        {
            if (isLocalPlayer)
            {
                SendInputs();
            }
        }
        private void SendInputs()
        {
            CollectedPlayerInput playerInput = CreatePlayerInput();
            if (playerInput != null)
            {
                CalculateVelocity(playerInput.Direction);
                clientSidePrediction.ReceiveVelocity(_velocity);
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

        [ClientCallback]
        private void OnMove(InputValue value) => _currentInput = value.Get<Vector2>();

        private void InitState()
        {
            //_state = new PlayerTransformState
            //{
            //    TimeStamp = 0,
            //    Position = transform.position,
            //};
        }

    }
}

