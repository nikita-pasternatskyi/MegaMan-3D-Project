using UnityEngine;
using Core.General;
namespace Core.Enemies
{
    [RequireComponent(typeof(ItemSpawner))]
    class Mettool : Enemy
    {
        [SerializeField] GameObject _projectile;
        [SerializeField] private float _timeToWait;
        [SerializeField] private ItemSpawner _itemSpawner;

        private float _currentTime;
        
        protected override void Attack()
        {
            Instantiate(_projectile);
        }

        protected override void Die()
        {
            _itemSpawner.SpawnItem();
            base.Die();
        }
    }
}