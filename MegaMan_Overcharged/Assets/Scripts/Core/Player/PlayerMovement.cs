using Core.ScriptableObjects;
using UnityEngine;

namespace Core.Player
{
    [System.Serializable]
    public class PlayerMovement
    {
        protected PlayerPhysics _playerPhysics;
        protected Transform _body;
        protected float _walkingSpeed;
        protected float _runSpeed;
        protected float _jumpHeight;
        protected bool _isRunning;

        protected Vector3 _velocity;

        public virtual void Start(in PlayerPhysics playerPhysics, in Transform body, PlayerClassConfiguration playerClassConfiguration)
        {
            _playerPhysics = playerPhysics;
            _jumpHeight = playerClassConfiguration.JumpHeight;
            _walkingSpeed = playerClassConfiguration.WalkSpeed;
            _runSpeed = playerClassConfiguration.RunSpeed;
            _body = body;
        }

        public PlayerMovement(in PlayerPhysics playerPhysics, in Transform body, PlayerClassConfiguration playerClassConfiguration)
        {
            _playerPhysics = playerPhysics;
            _jumpHeight = playerClassConfiguration.JumpHeight;
            _walkingSpeed = playerClassConfiguration.WalkSpeed;
            _runSpeed = playerClassConfiguration.RunSpeed;
            _body = body;
        }

        public void FixedUpdate(Vector2 movementInput)
        {
            CalculateVelocity(movementInput);
            _playerPhysics.AddVelocity(_velocity);
        }

        public void Jump()
        {
            if (_playerPhysics.IsGrounded)
            {
                _playerPhysics.AddVelocity(_jumpHeight * _body.up);
            }
        }

        private void CalculateVelocity(Vector2 movementInput)
        {
            var direction = _body.forward * movementInput.y + _body.right * movementInput.x;
            direction.Normalize();
            var speed = _walkingSpeed;
            if (_playerPhysics.IsGrounded)
                speed = _isRunning ? _runSpeed : _walkingSpeed;
            _velocity = new Vector3(direction.x * speed, _velocity.y, direction.z * speed);
        }
    }


}
