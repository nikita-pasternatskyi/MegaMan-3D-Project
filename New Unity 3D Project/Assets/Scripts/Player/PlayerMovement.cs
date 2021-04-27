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
        [SerializeField] private PlayerClass _playerClassConfiguration;
        [SerializeField] private PlayerPhysics _playerPhysics;
        [SerializeField] private Transform _forwardDirectionReference;
        private Vector3 _velocity;
        private Vector2 _currentInput;
        private PlayerClassConfiguration _currentClassConfiguration;

        private void Awake()
        {
            _currentClassConfiguration = _playerClassConfiguration.CurrentPlayerClass;
        }

        private void FixedUpdate()
        {
            if (isLocalPlayer)
            {
                SendVelocityToPhysics();
            }
        }

        [ClientCallback]
        private void SendVelocityToPhysics()
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
            _velocity = new Vector3(direction.x * _currentClassConfiguration.Speed, _velocity.y, direction.z * _currentClassConfiguration.Speed);
            //if (!_playerPhysics.IsGrounded)
            //    _velocity *= _currentClassConfiguration.AirMovementControlMultiplier;
        }

        [ClientCallback]
        private Vector2 CreatePlayerInputDirection()
        {
            Vector2 input = _currentInput;
            //input.Normalize();
            if (input == Vector2.zero)
                return Vector2.zero;
            return input;
        }

        [ClientCallback]
        private void OnMove(InputValue value)
        {      
            _currentInput = value.Get<Vector2>();
        }

        //private void OnJump() => Jump();

        //private void Jump()
        //{
        //    if (isLocalPlayer && _playerPhysics.IsGrounded)
        //    {                            
        //        _playerPhysics.AddVelocity(transform.up * _currentClassConfiguration.JumpHeight);
        //    }
        //}


    }
}

