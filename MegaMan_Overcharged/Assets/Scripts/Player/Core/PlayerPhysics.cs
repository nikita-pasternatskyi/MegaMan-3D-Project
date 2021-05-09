using UnityEngine;

namespace Core.Player
{
    class PlayerPhysics : MonoBehaviour
    {
        [SerializeField] private CurrentPlayerClass _currentPlayerClass;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private float _gravity;
        private Vector3 _velocity;
        private bool _isGrounded;
        public bool IsGrounded { get => _isGrounded; private set { } }

        private void FixedUpdate()
        {
            CheckGround();
            CalculateGravity();
            _characterController.Move(_velocity * Time.fixedDeltaTime);
        }

        private void CalculateGravity()
        {
            if (_isGrounded && _velocity.y < 0)
            {
                ApplyDrag(_currentPlayerClass.CurrentPlayerClassConfiguration.GroundDrag);
                _velocity.y = -.5f;
            }
            else if (!_isGrounded)
            {
                ApplyDrag(_currentPlayerClass.CurrentPlayerClassConfiguration.AirDrag);
                _velocity.y += _gravity * Time.fixedDeltaTime * _currentPlayerClass.CurrentPlayerClassConfiguration.Mass;
            }
        }

        private void CheckGround()
        {
            Vector3 groundCheckPosition = new Vector3
            (_characterController.bounds.center.x,
            _characterController.bounds.center.y - _characterController.height / 2,
            _characterController.bounds.center.z);

            _isGrounded = Physics.CheckSphere(groundCheckPosition, _groundCheckRadius, _whatIsGround);
        }

        public void AddVelocity(Vector3 velocityToAdd)
        {
            _velocity += velocityToAdd;
        }

        private void ApplyDrag(float drag)
        {
            float multiplier = 1.0f - drag * Time.fixedDeltaTime;
            if (multiplier < 0.0f) multiplier = 0.0f;
            _velocity.x *= multiplier;
            _velocity.z *= multiplier;
        }
    }
}
