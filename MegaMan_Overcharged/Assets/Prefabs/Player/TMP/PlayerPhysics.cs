using Mirror;
using UnityEngine;

namespace Assets.Prefabs.Player.TMP
{
    public class PlayerPhysics : NetworkBehaviour
    {
        [SerializeField] private PlayerClassConfiguration _currentClassConfiguration;
        [SerializeField] private ClientMovementPrediction _clientSidePrediction;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private LayerMask _whatIsGround;

        [SerializeField] private float _groundCheckRadius = 0.1f;
        [SerializeField] private float _fixedDeltaTimeRate = .02f;

        [SyncVar] private bool _isGrounded;
        [SyncVar] private Vector3 _velocity;
        public bool IsGrounded { get => _isGrounded; }

        private void ApplyDrag(float drag)
        {
            float multiplier = 1.0f - drag * Time.fixedDeltaTime;
            if (multiplier < 0.0f) multiplier = 0.0f;
            _velocity *= multiplier;
        }

        [Command]
        private void CalculatePhysics()
        {
            print(IsGrounded);
            CheckGround();
            if (_isGrounded && _velocity.y < 0)
            {
                ApplyDrag(_currentClassConfiguration.GroundDrag);
                _velocity.y = 0f;
            }

            if (!_isGrounded)
            {
                ApplyDrag(_currentClassConfiguration.AirDrag);
                _velocity.y += _currentClassConfiguration.Gravity * Time.fixedDeltaTime * _currentClassConfiguration.Mass;
            }
        }

        private void FixedUpdate()
        {
            if (isLocalPlayer)
            {
                CheckGround();
                CalculatePhysics();
                CmdMoveOnServer();
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

        [Command]
        public void AddVelocity(Vector3 velocityToAdd)
        {
            if (velocityToAdd == Vector3.zero)
            {
                _velocity = Vector3.zero;
            }
            _velocity += velocityToAdd;
        }

        [Command]
        private void CmdMoveOnServer()
        {
            _characterController.Move(_velocity);
        }
    }
}
