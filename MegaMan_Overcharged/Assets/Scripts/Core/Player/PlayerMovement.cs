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
        protected float _deltaTime;
        protected bool _isRunning;

        protected Vector3 _velocity;

        public PlayerMovement(in PlayerPhysics playerPhysics, in Transform body, float jumpHeight, float walkingSpeed, float runSpeed)
        {
            _playerPhysics = playerPhysics;
            _jumpHeight = jumpHeight;
            _walkingSpeed = walkingSpeed;
            _runSpeed = runSpeed;
            _body = body;
        }

        public void FixedUpdate(Vector2 movementInput, float deltaTime)
        {
            _deltaTime = Time.fixedDeltaTime;
            CalculateVelocity(movementInput);
            _playerPhysics.AddVelocity(_velocity);
        }

        public void Jump()
        {
            if (_playerPhysics.IsGrounded)
            {
                _playerPhysics.AddVelocity(_jumpHeight * _body.up * _deltaTime);
            }
        }

        private void CalculateVelocity(Vector2 movementInput)
        {
            var direction = _body.forward * movementInput.y + _body.right * movementInput.x;
            direction.Normalize();
            var speed = _isRunning ? _runSpeed : _walkingSpeed;
            _velocity = new Vector3(direction.x * speed, _velocity.y, direction.z * speed);
        }
    }


}
