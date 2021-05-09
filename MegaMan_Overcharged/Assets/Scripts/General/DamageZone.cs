using UnityEngine;

namespace Core.General
{
    public class DamageZone : MonoBehaviour
    {
        [SerializeField] private int _damage;

        private void OnTriggerEnter(Collider other)
        {
             if (other.GetComponent<Health>() != null)
             {
                other.GetComponent<Health>().TakeDamage(_damage);
             }
        }

    }
}
