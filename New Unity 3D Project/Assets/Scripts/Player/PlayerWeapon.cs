using Assets.Scripts.Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject _projectile;
        [SerializeField] private Transform _whereToSpawn;
        [SerializeField] private Transform _referenceRotation;
        [SerializeField] private float _maxAmmo;
        [SerializeField] private float _currentAmmo;
        public virtual void Refill(float ammoToRefill)
        {
            _currentAmmo = _currentAmmo + ammoToRefill < _maxAmmo ? _currentAmmo + ammoToRefill : _maxAmmo;
            RefreshWeaponUI();
        }

        protected virtual void OnEnable()
        {
            Input.FirePressed += MainFire;
        }
        protected virtual void RefreshWeaponUI()
        { 
        
        }
        protected virtual void MainFire()
        {
            if (!LevelSettings.Instance.IsPaused)
            {
                Instantiate(_projectile, _whereToSpawn.position, _referenceRotation.rotation);
            }
        }
        protected virtual void AltFire()
        {
            if (!LevelSettings.Instance.IsPaused)
            {
            
            }
        }
        protected virtual void OnDisable()
        {
            Input.FirePressed -= MainFire;
        }

   
    }
}
