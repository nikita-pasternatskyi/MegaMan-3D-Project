using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject _projectile;
        [SerializeField] private Transform _whereToSpawn;
        [SerializeField] private float _maxAmmo;
        [SerializeField] private float _currentAmmo;
        public virtual void Refill(float ammoToRefill)
        {
            _currentAmmo += ammoToRefill;
            RefreshWeaponUI();
        }

        private void OnEnable()
        {
            Input.FirePressed += MainFire;
        }

        protected virtual void RefreshWeaponUI()
        { 
        
        }

        protected virtual void MainFire()
        {
            Instantiate(_projectile, _whereToSpawn.position, _whereToSpawn.rotation);
        }

        protected virtual void AltFire()
        { 
            
        }
    }
}
