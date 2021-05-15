using UnityEngine;

namespace Core.Player
{
    [System.Serializable]
    public class PlayerMovement
    {
        private PlayerPhysics _playerPhysics;
        private Transform _body;
        private float _walkingSpeed;
        private float _runSpeed;
        private float _jumpHeight;
        private float _deltaTime;

        private Vector3 _velocity;

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
            _deltaTime = deltaTime;
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
            _velocity = new Vector3(direction.x * _runSpeed, _velocity.y, direction.z * _runSpeed);
        }
    }


}
