using Core.Player;
using UnityEngine;

namespace NonCore.Player.MegaMan
{
    class RushCoil : MonoBehaviour
    {
        [SerializeField] private float _jumpHeight;

        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<PlayerPhysics>()?.AddVelocity(_jumpHeight * transform.up);
        }
    }
}
