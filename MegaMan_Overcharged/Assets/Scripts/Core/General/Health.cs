using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Core.General
{
    public class Health : MonoBehaviour, IHasHealth
    {
        [SerializeField] private int _maxHealth;
        public event Action ActorKilled;
        public event Action<int, float> HealthChanged;
        public UnityEvent<int, float> ObjectHealthChanged;
        public UnityEvent ObjectKilled;
        
        private int _currentHealth;
        

        private void Start()
        {
            _currentHealth = _maxHealth;
            HealthChanged += ObjectHealthChanged.Invoke;
            ActorKilled += ObjectKilled.Invoke;
            HealthChanged?.Invoke(_currentHealth, 1);
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                Die();
            }

            HealthChanged?.Invoke(_currentHealth, (float)_currentHealth / _maxHealth);
        }
        public void Heal(int healthToHeal)
        {
            _currentHealth = _currentHealth + healthToHeal < _maxHealth ? _currentHealth + healthToHeal : _maxHealth;
            HealthChanged?.Invoke(_currentHealth, (float)_currentHealth / _maxHealth);
        }
        private void Die()
        {                   
            ActorKilled?.Invoke();
        }

        private void OnDisable()
        {
            HealthChanged -= ObjectHealthChanged.Invoke;
            ActorKilled -= ObjectKilled.Invoke;
        }
    }
}