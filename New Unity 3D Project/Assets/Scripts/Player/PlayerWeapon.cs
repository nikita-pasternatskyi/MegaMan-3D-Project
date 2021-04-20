using Assets.Scripts.Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
namespace Assets.Scripts.Player
{
    class PlayerWeapon : NetworkBehaviour
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

        protected virtual void RefreshWeaponUI()
        { 
        
        }

        protected virtual void OnMainFire()
        {
            if (!LevelSettings.Instance.IsPaused)
            {
                CreateProjectile(_projectile);
            }
        }

        protected virtual void OnAltFire()
        {
            if (!LevelSettings.Instance.IsPaused)
            {
            
            }
        }

        [Command]
        protected virtual void CreateProjectile(GameObject projectile)
        {
            NetworkServer.Spawn(Instantiate(_projectile, new Vector3(0,0,0), new Quaternion(0,0,0, transform.rotation.w)));
        }
   
    }
}
