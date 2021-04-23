using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(PlayerPhysics))]
    public class PlayerMovement : NetworkBehaviour
    {
        [SerializeField] private PlayerPhysics _playerPhysics;
        [SerializeField] private Transform _forwardDirectionReference;
        [SerializeField] private float _speed = .5f;
        [SerializeField] private float _sprintSpeedMultiplier;
        [SerializeField] private float _jumpHeight = 3;
        [SerializeField] private float _airMultiplier = 0.4f;

        private Vector3 _velocity;
        private Vector2 _currentInput;

        private void FixedUpdate()
        {
            if (isLocalPlayer)
            {
                SendInputs();
            }
        }

        [ClientCallback]
        private void SendInputs()
        {
            Vector2 direction = CreatePlayerInputDirection();
            if (direction != Vector2.zero)
            {
                CalculateVelocity(direction);
                _playerPhysics.AddVelocity(_velocity);
            };
        }

        private void CalculateVelocity(Vector3 input)
        {
            var direction = _forwardDirectionReference.forward * input.y + _forwardDirectionReference.right * input.x;
            _velocity = new Vector3(direction.x * _speed, _velocity.y, direction.z * _speed);
            if (!_playerPhysics.IsGrounded)
                _velocity *= _airMultiplier;
        }

        [ClientCallback]
        private Vector2 CreatePlayerInputDirection()
        {
            Vector2 input = _currentInput;
            input.Normalize();
            if (input == Vector2.zero)
                return Vector2.zero;
            return input;
        }

        [ClientCallback]
        private void OnMove(InputValue value)
        {      
            _currentInput = value.Get<Vector2>();
        }

        [ClientCallback]
        private void OnJump() => Jump();

        private void Jump()
        {
            if (isLocalPlayer && _playerPhysics.IsGrounded)
            {                            
                _playerPhysics.AddVelocity(transform.up * _jumpHeight);
            }
        }


    }
}

