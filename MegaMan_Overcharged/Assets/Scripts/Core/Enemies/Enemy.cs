using UnityEngine;
using Core.General;
using Core.ScriptableObjects;
using Core.Interfaces;

namespace Core.Enemies
{
    public class Enemy : MonoBehaviour, IHasHealth
    {
        [SerializeField] private EnemyConfiguration _enemyConfiguration;
        [SerializeField] private Animator _animator;
        [SerializeField] EnemyState _currentEnemyState;
        private InternalHealth _health;

        private void Start() => _health = new InternalHealth(_enemyConfiguration.Health);

        private void OnEnable() => _health.ActorKilled += Die;

        protected virtual void Attack()
        { 
        
        }

        protected virtual void Die()
        {

        }

        private void OnDisable() => _health.ActorKilled -= Die;

        public void TakeDamage(int damage) => _health.TakeDamage(damage);

        public void Heal(int healCount) => _health.Heal(healCount);
    }
}
