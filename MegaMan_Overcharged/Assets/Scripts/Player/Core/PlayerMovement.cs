using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Player
{

    public class PlayerMovement : MonoBehaviour
    {
        [Header("Class dependencies")]
        [SerializeField] private PlayerPhysics _playerPhysics;
        [SerializeField] private CurrentPlayerClass _currentPlayerClass;
        [SerializeField] private Transform _referenceTransform;

        private Vector2 _movementInput;
        private Vector3 _velocity;

        private void OnJump()
        {
            if (_playerPhysics.IsGrounded) 
            {
                _playerPhysics.AddVelocity(_currentPlayerClass.CurrentPlayerClassConfiguration.JumpHeight * transform.up * Time.fixedDeltaTime);
            }
        }

        private void FixedUpdate()
        {
            CalculateVelocity();
            _playerPhysics.AddVelocity(_velocity);
        }

        private void CalculateVelocity()
        {
            var direction = _referenceTransform.forward * _movementInput.y + _referenceTransform.right * _movementInput.x;
            float speed = _currentPlayerClass.CurrentPlayerClassConfiguration.Speed;
            direction.Normalize();
            _velocity = new Vector3(direction.x * speed, _velocity.y, direction.z * speed);
        }

        private void OnMovement(InputValue value)
        {
            _movementInput = value.Get<Vector2>();          
        }

    }


}
