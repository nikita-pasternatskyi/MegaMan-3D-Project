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
    class PlayerPhysics : NetworkBehaviour
    {        
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private float _gravity;
        [SerializeField] private float _mass;
        public float jumpHeight;
        public Vector3 _velocity;

        private bool _isGrounded;

        public override void OnStartAuthority()
        {
            base.OnStartAuthority();
            enabled = true;
        }

        [ClientCallback]
        private void FixedUpdate()
        {
            if (isLocalPlayer)
            {
                CheckGround();
                CalculatePhysics();
            }
        }

        [Server]
        private void CmdMoveOnServer()
        {
            if (isLocalPlayer)
            {
                _velocity.y += _gravity * Time.fixedDeltaTime;
                _characterController.Move(_velocity * Time.fixedDeltaTime);
            }
        }

        [Command]
        private void CalculatePhysics()
        {
            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            else if (!_isGrounded)
            {
                _velocity.y += _gravity * Time.fixedDeltaTime;
                CmdMoveOnServer();
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

        //[ClientCallback]
        //private void OnJump()
        //{
        //    //if (_isGrounded)
        //    //{
        //    //    _velocity.y = Mathf.Sqrt(jumpHeight * -2 * _gravity);
        //    //}
        //}

    }
}
