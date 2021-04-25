using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mirror;
using UnityEngine;


namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerPhysics : NetworkBehaviour
    {

        [SerializeField] private ClientSidePrediction _clientSidePrediction;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private float _groundCheckRadius = 0.1f;
        [SerializeField] private float _gravity = -9.8f;
        [SerializeField] private float _mass = 1;
        [SerializeField] private float _fixedDeltaTimeRate = .02f;
        [SerializeField] private float _groundDrag = 6;
        [SerializeField] private float _airDrag = 3;

        [SyncVar] private Vector3 _velocity;
        [SyncVar] private bool _isGrounded;

        public bool IsGrounded { get => _isGrounded; }

        private void ApplyDrag(float drag)
        {
            float multiplier = 1.0f - drag * _fixedDeltaTimeRate;
            if (multiplier < 0.0f) multiplier = 0.0f;
            _velocity = multiplier * _velocity;
        }

        public override void OnStartAuthority()
        {
            base.OnStartAuthority();
        }

        [Command]
        private void CalculatePhysics()
        {
            if (_isGrounded && _velocity.y < 0)
            {
                ApplyDrag(_groundDrag);
                _velocity.y = -2;
            }

            if (!_isGrounded)
            {
                ApplyDrag(_airDrag);
                _velocity.y += _gravity * Time.fixedDeltaTime * _mass;
            }
        }

        private void PhysicsUpdate()
        {
            CheckGround();
            CalculatePhysics();
            _clientSidePrediction.ReceiveVelocity(_velocity);
        }

        private void FixedUpdate() 
        {
            if (isLocalPlayer)
            {
                CheckGround();
                CalculatePhysics();
                _clientSidePrediction.ReceiveVelocity(_velocity);
            }
        }

        public void AddVelocity(Vector3 velocityToAdd)
        { 
            if (isLocalPlayer)
                _velocity += velocityToAdd;
        }

        [Command]
        private void CheckGround()
        {
            Vector3 groundCheckPosition = new Vector3
                    (_characterController.bounds.center.x,
                    _characterController.bounds.center.y - _characterController.height / 2,
                    _characterController.bounds.center.z);

            _isGrounded = Physics.CheckSphere(groundCheckPosition, _groundCheckRadius, _whatIsGround);
        }
    }
}