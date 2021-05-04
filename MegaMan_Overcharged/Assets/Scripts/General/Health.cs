using Assets.Scripts.Levels;
using System;
using UnityEngine;
using Mirror;

namespace Assets.Scripts.General
{

    public class Health : NetworkBehaviour
    {
        public delegate void OnHealthChanged(int health, float uiPercentage);
        public event OnHealthChanged HealthChanged;
        
        public delegate void OnKilled();
        public event OnKilled Killed;

        [SerializeField] private int _maximumHealth;
        [SyncVar(hook = "OnServerHealthChanged")] [SerializeField] private int _currentHealth;

        private void Start()
        {

        }
        [ClientCallback]
        protected virtual void Awake()
        {
            HealthChanged?.Invoke(_maximumHealth, 1);
        }

        [ClientCallback]
        public void OnServerHealthChanged(int oldHealth, int currentHealth)
        {
            _currentHealth = currentHealth;
            HealthChanged?.Invoke(currentHealth, (float)currentHealth / _maximumHealth);
        }

        [Command]
        public virtual void Heal(int healthToHeal)
        {
            _currentHealth = _currentHealth + healthToHeal < _maximumHealth ? _currentHealth + healthToHeal : _maximumHealth;
            HealthChanged?.Invoke(_currentHealth, (float)_currentHealth / _maximumHealth);
        }

        [Command]
        protected virtual void Die()
        {
            Killed?.Invoke();
        }
        public virtual void TakeDamage(int damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                Die();
            }
        }
    }

}
