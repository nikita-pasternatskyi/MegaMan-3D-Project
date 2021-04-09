using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.General;

namespace Assets.Scripts
{
    public class DamageZone : MonoBehaviour
    {
        [SerializeField] private int _damage;

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<Health>() != null)
            {
                other.GetComponent<Health>().TakeDamage(_damage);
            }
        }

    }
}
