﻿using Core.Interfaces;
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
        private InternalHealth _health;
        public UnityEvent<int, float> ObjectHealthChanged;
        public UnityEvent ObjectKilled;
        private void Start()
        {
            _health = new InternalHealth(_maxHealth);
            _health.Start();
            _health.ActorKilled += ObjectKilled.Invoke;
            _health.HealthChanged += ObjectHealthChanged.Invoke;
            _health.ActorKilled += Kill;
        }
        public void TakeDamage(int damage) => _health.TakeDamage(damage);
        public void Heal(int healCount) => _health.Heal(healCount);

        protected virtual void Kill()
        {
            Destroy(this.gameObject);
        }

        private void OnDisable()
        {
            _health.ActorKilled -= ObjectKilled.Invoke;
            _health.HealthChanged -= ObjectHealthChanged.Invoke;
            _health.ActorKilled -= Kill;
        }
    }
}