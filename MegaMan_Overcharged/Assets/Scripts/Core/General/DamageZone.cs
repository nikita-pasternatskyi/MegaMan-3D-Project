using Core.Interfaces;
using UnityEngine;

namespace Core.General
{
    public class DamageZone : MonoBehaviour
    {
        [SerializeField] private int _damage;

        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<IHasHealth>()?.TakeDamage(_damage);
        }

    }
}
