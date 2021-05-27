using Core.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Player
{
    [RequireComponent(typeof(PlayerPhysics))]
    public abstract class PlayerMovement : RequiresInput
    {
        protected PlayerPhysics _playerPhysics;
        [SerializeField] protected Transform _referenceDirectionTransform;
        [SerializeField] protected float _walkingSpeed;
        [SerializeField] protected float _runSpeed;
        [SerializeField] protected float _jumpHeight;

        protected bool _isRunning;

        protected Vector2 _input;

        private void Start()
        {
            _playerPhysics = GetComponent<PlayerPhysics>();
        }
        private new void OnMovement(InputValue value)
        {
            _input = value.Get<Vector2>();
        }
        private Vector3 CalculateVelocity(Vector2 movementInput)
        {
            var direction = _referenceDirectionTransform.forward * movementInput.y + _referenceDirectionTransform.right * movementInput.x;
            direction.Normalize();
            var speed = _walkingSpeed;
            if (_playerPhysics.IsGrounded)
                speed = _isRunning ? _runSpeed : _walkingSpeed;
            return new Vector3(direction.x * speed, 0, direction.z * speed);
        }
        protected virtual void FixedUpdate()
        {
            if (isActiveAndEnabled)
            {
                _playerPhysics.AddVelocity(CalculateVelocity(_input));
            }
        }
        protected override void OnJump()
        {
            if (_playerPhysics.IsGrounded && isActiveAndEnabled)
            {
                _playerPhysics.AddVelocity(_jumpHeight * _referenceDirectionTransform.up);
            }
        }

    }


}
