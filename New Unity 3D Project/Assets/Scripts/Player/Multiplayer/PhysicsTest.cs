using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mirror;
using UnityEngine;


namespace Assets.Scripts.Player.Multiplayer
{
    class PhysicsTest : NetworkBehaviour
    {
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private float _gravity;
        [SerializeField] private float _mass;
        [SerializeField] private float _fixedDeltaTimeRate;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private Vector3 _velocity;
        [SerializeField] private float _speed;
        [SerializeField] private CharacterController _characterController;
        [SyncVar] private bool _isGrounded;

        public override void OnStartAuthority()
        {
            base.OnStartAuthority();
            enabled = true;
            StartCoroutine(WaitForGrounded());
        }

        private IEnumerator WaitForGrounded()
        {
            do
            {
                CheckGround();
                ProcessPhysics();
                CmdMoveServer();
                yield return new WaitForFixedUpdate();
            }
            while (_isGrounded != true);
        }

        [Command]
        private void CmdMoveServer()
        {
            _characterController.Move(_velocity * _fixedDeltaTimeRate);
        }

        [Command]
        private void ProcessPhysics()
        {
            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            if (!_isGrounded)
            {
                _velocity.y += _gravity;       
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
    }
}