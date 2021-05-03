using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.General;
using Assets.Scripts.Levels;
using Mirror;


namespace Assets.Scripts
{
    public class DamageZone : NetworkBehaviour
    {
        [SerializeField] private int _damage;

        private void OnTriggerEnter(Collider other)
        {
             if (other.GetComponent<Health>() != null)
             {
                //other.GetComponent<Health>().TakeDamage(_damage);
             }
        }

    }
}
