using UnityEngine;

namespace Core.Player
{
    [System.Serializable]
    public class PlayerMovement
    {
        protected Rigidbody _playerRigidbody;
        protected Transform _body;
        protected float _walkingSpeed;
        protected float _runSpeed;
        protected float _jumpHeight;
        protected float _airMultiplier;
        protected bool _isRunning;
        public void Start(in Rigidbody playerRigidbody, in Transform body, float jumpHeight, float walkingSpeed, float runSpeed, in float airMultiplier)
        {
            _airMultiplier = airMultiplier;
            _playerRigidbody = playerRigidbody;
            _jumpHeight = jumpHeight;
            _walkingSpeed = walkingSpeed;
            _runSpeed = runSpeed;
            _body = body;
        }

        public PlayerMovement(in Rigidbody playerRigidbody, in Transform body, float jumpHeight, float walkingSpeed, float runSpeed, in float airMultiplier)
        {
            _airMultiplier = airMultiplier;
            _playerRigidbody = playerRigidbody;
            _jumpHeight = jumpHeight;
            _walkingSpeed = walkingSpeed;
            _runSpeed = runSpeed;
            _body = body;
        }

        public virtual void Move(Vector2 movementInput, bool isGrounded)
        {
            _playerRigidbody.AddForce(CalculateVelocity(movementInput, isGrounded), ForceMode.VelocityChange);
        }

        public virtual void Jump() => _playerRigidbody.AddForce(_jumpHeight * _body.up, ForceMode.VelocityChange);

        protected virtual Vector3 CalculateVelocity(Vector2 movementInput, bool isGrounded)
        {
            var direction = _body.forward * movementInput.y + _body.right * movementInput.x;
            direction.Normalize();
            var speed = _isRunning ? _runSpeed : _walkingSpeed;
            speed *= isGrounded ? 1 : _airMultiplier;
            return new Vector3(direction.x * speed, 0, direction.z * speed);
        }


    }


}
