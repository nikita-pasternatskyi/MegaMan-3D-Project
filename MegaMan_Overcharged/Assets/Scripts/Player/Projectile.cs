using UnityEngine;
using Core.General;

namespace Core.Projectile
{
    [AddComponentMenu("Objects/Projectile")]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;
        [SerializeField] private float _lifeTime;

        private void FixedUpdate()
        {
            transform.position += transform.forward * _speed * Time.fixedDeltaTime;
            Destroy(gameObject, _lifeTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<Health>()?.TakeDamage(_damage);
        }

    }
}
