using Core.Levels;
using System;
using UnityEngine;

namespace Core.General
{
    [AddComponentMenu("Player/Base/Health")]

    class Health : MonoBehaviour
    {
        public delegate void OnHealthChanged(int health, float percentage);
        public delegate void OnKilled();

        public event OnHealthChanged HealthChanged;
        public event OnKilled Killed;

        [SerializeField] private int _maximumHealth;
        private int _currentHealth;

        protected virtual void Awake()
        {
            HealthChanged?.Invoke(_maximumHealth, 1);
            _currentHealth = _maximumHealth;
        }

        public virtual void TakeDamage(int damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                Die();
            }

            HealthChanged?.Invoke(_currentHealth, (float)_currentHealth / _maximumHealth);
        }

        public virtual void Heal(int healthToHeal)
        {
            _currentHealth = _currentHealth + healthToHeal < _maximumHealth ? _currentHealth + healthToHeal : _maximumHealth;
            HealthChanged?.Invoke(_currentHealth, (float)_currentHealth / _maximumHealth);
        }

        protected virtual void Die()
        {
            Killed?.Invoke();
            Destroy(this.gameObject);
        }

    }
}
