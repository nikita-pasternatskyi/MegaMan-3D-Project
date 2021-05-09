using UnityEngine;

namespace NonCore.Player.MegaMan
{
    class RushCoil : MonoBehaviour
    {
        [SerializeField] private float _jumpHeight;

        private void OnTriggerEnter(Collider other)
        {
            //if (other.GetComponent<PlayerMove>() != null)
            //{
            //    other.GetComponent<PlayerMove>().Jump(_jumpHeight);
            //}
        }

    }
}
