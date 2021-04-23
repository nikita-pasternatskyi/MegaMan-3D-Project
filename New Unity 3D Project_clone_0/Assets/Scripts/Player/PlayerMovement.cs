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
        private float _currentJump;
        private Vector3 _velocity;
        private Vector2 _currentInput;
        public float airMultiplier = 0.4f;

        [Header("Class dependencies")]
        public ClientSidePrediction clientSidePrediction;
        public PhysicsTest physicsTest;

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
                physicsTest.AddVelocity(_velocity);
            };
        }

        private void CalculateVelocity(Vector3 input)
        {
            var direction = _forwardDirectionReference.forward * input.y + _forwardDirectionReference.right * input.x;
            _velocity = new Vector3(direction.x * _speed, _currentJump, direction.z * _speed);
            if (!physicsTest.IsGrounded)
                _velocity *= airMultiplier;

        }

        private CollectedPlayerInput CreatePlayerInput()
        {
            CollectedPlayerInput playerInput = new CollectedPlayerInput();
            playerInput.Direction = _currentInput;
            playerInput.JumpPower = _currentJump;
            if (playerInput.Direction == Vector2.zero && playerInput.JumpPower == 0)
                return null;
            return playerInput;
        }

        [ClientCallback]
        private void OnMove(InputValue value)
        {      
            _currentInput = value.Get<Vector2>();
        }

        [ClientCallback]
        private void OnJump() => Jump();

        [Command]
        private void Jump()
        {
            if (isLocalPlayer && physicsTest.IsGrounded)
            {                            
                physicsTest.AddVelocity(transform.up * _jumpHeight);
            }
        }
    }
}

