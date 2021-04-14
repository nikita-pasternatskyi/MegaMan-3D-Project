using UnityEngine;
using Assets.Scripts.General;
using Mirror;

namespace Assets.Scripts.Player
{
    [AddComponentMenu("Objects/Projectile")]
    public class Projectile : NetworkBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;
        [SerializeField] private float _lifeTime;

        private void FixedUpdate()
        {
            _lifeTime -= Time.fixedDeltaTime;
            transform.position += transform.forward * _speed * Time.fixedDeltaTime;
            if (_lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<Health>()?.TakeDamage(_damage);
        }

    }
}
