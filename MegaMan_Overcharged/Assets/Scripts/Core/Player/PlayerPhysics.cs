using Core.ScriptableObjects;
using System.Threading;
using UnityEngine;

namespace Core.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerPhysics : MonoBehaviour
    {
        public Vector3 Velocity { get => _velocity; }
        public bool IsGrounded { get => isGrounded(); private set { } }

        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private float _gravity;
        [SerializeField] private bool _useGravity;

        [SerializeField] private float _mass;
        [SerializeField] private float _airDrag;
        [SerializeField] private float _groundDrag;

        private CharacterController _characterController;
        private Vector3 _velocity;
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

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
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