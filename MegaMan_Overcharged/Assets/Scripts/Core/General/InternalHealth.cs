using Core.Interfaces;
using Core.Levels;
using System;

namespace Core.General
{
    public class InternalHealth
    {
        public event Action ActorKilled;
        public event Action<int, float> HealthChanged;

        private int _maximumHealth;
        public int _currentHealth;

        public InternalHealth(int maximumHealth)
        {
            _maximumHealth = maximumHealth;
            _currentHealth = _maximumHealth;
        }
        public void Start()
        {
            HealthChanged?.Invoke(_maximumHealth, 1);
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                Die();
            }

            HealthChanged?.Invoke(_currentHealth, (float)_currentHealth / _maximumHealth);
        }

        public void Heal(int healthToHeal)
        {
            _currentHealth = _currentHealth + healthToHeal < _maximumHealth ? _currentHealth + healthToHeal : _maximumHealth;
            HealthChanged?.Invoke(_currentHealth, (float)_currentHealth / _maximumHealth);
        }

        private void Die()
        {
            ActorKilled?.Invoke();
        }

    }
}
