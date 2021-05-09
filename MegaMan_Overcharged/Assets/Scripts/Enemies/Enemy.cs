using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.General;

namespace Core.Enemies
{
    [RequireComponent(typeof(Health))]
    class Enemy : MonoBehaviour
    {

        [SerializeField] private Health _health;
        [SerializeField] private Animator _animator;
        [SerializeField] EnemyState _currentEnemyState;

        protected void OnEnable()
        {
            _health.Killed += Die;
        }

        protected virtual void Attack()
        { 
        
        }

        protected virtual void Die()
        { 
        
        }

        protected void OnDisable()
        {
            _health.Killed -= Die;
        }

    }
}
