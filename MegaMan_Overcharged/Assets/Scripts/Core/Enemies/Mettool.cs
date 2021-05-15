using UnityEngine;
using Core.General;
namespace Core.Enemies
{

    class Mettool : Enemy
    {
        [SerializeField] GameObject _projectile;
        [SerializeField] private float _timeToWait;

        private float _currentTime;
        
        protected override void Attack()
        {
        }

        protected override void Die()
        {

        }
    }
}