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
        [SerializeField] private bool usesAnimationToTriggerFlight = true;
        private bool _flight = false;

        private void Start() 
        {
            Destroy(gameObject, _lifeTime);
            if (!usesAnimationToTriggerFlight)
                ToggleFlight();
        }

        private void FixedUpdate()
        {
            if(_flight)
                FlyForward(_speed);
        }

        public void ToggleFlight() => _flight = true;

        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<IHasHealth>()?.TakeDamage(_damage);
        }
    }
}
