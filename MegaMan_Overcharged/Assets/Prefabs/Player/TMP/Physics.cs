using Mirror;
using UnityEngine;

namespace Assets.Prefabs.Player.TMP
{
    [System.Serializable]
    public class Physics
    {
        [SerializeField] [SyncVar] private LayerMask _whatIsGround;
        [SerializeField] [SyncVar] private float _groundCheckRadius = 0.1f;

        [SyncVar] private bool _isGrounded;
        [SyncVar] private Vector3 _velocity;

        public Vector3 Velocity { get => _velocity; set => _velocity += value; }
        public bool IsGrounded { get => _isGrounded; }


}
}
