using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mirror;
using UnityEngine;


namespace Assets.Scripts.Player
{
    public class PhysicsTest : NetworkBehaviour
    {

        [SerializeField] private ClientSidePrediction _clientSidePrediction;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private float _gravity;
        [SerializeField] private float _mass;
        [SerializeField] private float _fixedDeltaTimeRate;
        [SerializeField] private float _groundDrag;
        [SerializeField] private float _airDrag;

        [SyncVar] private Vector3 _velocity;
        [SyncVar] private bool _isGrounded;
        [SyncVar] private bool _simulationRunning;

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
            //StartCoroutine(ProcessPhysics());
        }

        [Command]
        private void CalculatePhysics()
        {
            if (_isGrounded && _velocity.y < 0)
            {
                ApplyDrag(_groundDrag);
                _velocity.y = -2f;
            }

            if (!_isGrounded)
            {
                ApplyDrag(_airDrag);
                _velocity.y += _gravity * _fixedDeltaTimeRate;            
            }
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

        private void PhysicsUpdate()
        {
            CheckGround();
            _clientSidePrediction.ReceiveVelocity(_velocity);
            CalculatePhysics();
        }

        private void FixedUpdate()
        {
            PhysicsUpdate();
        }

        public void AddVelocity(Vector3 velocityToAdd)
        {
            _velocity += velocityToAdd;
        }
    }
}