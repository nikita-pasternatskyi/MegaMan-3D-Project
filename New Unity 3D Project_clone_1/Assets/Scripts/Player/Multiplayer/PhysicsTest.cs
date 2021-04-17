using System;
using System.Collections.Generic;
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
        public float jumpHeight;
        public Vector3 _velocity;
        public float speed;
        public CharacterController ch;
        private bool _isGrounded;

        public override void OnStartAuthority()
        {
            base.OnStartAuthority();
            enabled = true;
        }
            
        private void FixedUpdate()
        {
            if (isLocalPlayer)
            {
                ProcessPhysics();
                CmdMoveServer();
            }
        }

        [Command]
        private void CmdMoveServer()
        {
            ch.Move(_velocity);
        }
        [Command]
        private void ProcessPhysics()
        {
            CheckGround();

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            if (!_isGrounded)
            {
                _velocity.y += _gravity;
                
            }
        }


        private void CheckGround()
        {
            Vector3 groundCheckPosition = new Vector3
                    (ch.bounds.center.x,
                    ch.bounds.center.y - ch.height / 2,
                    ch.bounds.center.z);

            _isGrounded = Physics.CheckSphere(groundCheckPosition, _groundCheckRadius, _whatIsGround);
        }
    }
}
