using Assets.Scripts.Levels;
using System;
using UnityEngine;
using Mirror;

namespace Assets.Scripts.Player.MegaMan
{
    class RushCoil : NetworkBehaviour
    {
        [SerializeField] private float _jumpHeight;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerMovement>() != null)
            {
               // other.GetComponent<PlayerMove>().Jump(_jumpHeight);
            }

            else
            {

            }

        }

    }
}
