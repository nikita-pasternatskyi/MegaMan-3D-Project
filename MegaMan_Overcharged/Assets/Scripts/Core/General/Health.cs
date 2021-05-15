using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.General
{
    public class Health : MonoBehaviour, IHasHealth
    {
        [SerializeField] private InternalHealth _health;
        [SerializeField] private int _maxHealth;
        private void Start()
        {
            _health = new InternalHealth(_maxHealth);
            _health.Start();
        }

        public void TakeDamage(int damage) => _health.TakeDamage(damage);

        public void Heal(int healCount) => _health.Heal(healCount);
    }
}
