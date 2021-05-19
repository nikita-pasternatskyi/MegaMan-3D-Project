using System.Threading;
using UnityEngine;

namespace Core.Player
{
    [System.Serializable]
    public class PlayerPhysics
    {
        public bool IsGrounded { get => isGrounded(); private set { } }
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private float _gravity;
        public Vector3 Velocity { get => _velocity; }

        private CharacterController _characterController;
        private Vector3 _velocity;
        private float _mass;
        private float _airDrag;
        private float _groundDrag;
        private bool _useGravity;
        private bool isGrounded()
        {
            if (_characterController)
            {
                Vector3 groundCheckPosition = new Vector3
                (_characterController.bounds.center.x,
                _characterController.bounds.center.y - _characterController.height / 2,
                _characterController.bounds.center.z);
                return Physics.CheckSphere(groundCheckPosition, _groundCheckRadius, _whatIsGround);
            }
            return false;
        }

        public void Start(float mass, float airDrag, float groundDrag, in CharacterController characterController)
        {
            _useGravity = true;
            _mass = mass;
            _airDrag = airDrag;
            _groundDrag = groundDrag;
            _characterController = characterController;
        }

        public void AddVelocity(Vector3 velocityToAdd)
        {
            _velocity += velocityToAdd;
        }
        public void FixedUpdate()
        {
            CalculateGravity(Time.fixedDeltaTime);
            _characterController.Move(_velocity * Time.fixedDeltaTime);
        }
        public void ResetYVelocity()
        {
            _velocity.y = 0;
        }

        private void CalculateGravity(float deltaTime)
        {
            if (isGrounded() && _velocity.y < 0)
            {
                ApplyDrag(_groundDrag);
                if (_useGravity)
                    _velocity.y = -.5f;
            }
            else if (!isGrounded())
            {
                ApplyDrag(_airDrag);
                if (_useGravity)
                    _velocity.y += _gravity * deltaTime * _mass;
            }
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