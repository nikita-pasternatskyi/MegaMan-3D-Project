using Core.Interfaces;
using Core.Player;
using UnityEngine;

namespace NonCore.Player.MegaMan
{
    class MegaBusterProjectile : Projectile
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;

        private void Start() => Destroy(gameObject, _lifeTime);

        private void FixedUpdate() => FlyForward(_speed);

        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<IHasHealth>()?.TakeDamage(_damage);
        }
    }
}
