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
            if (isLocalPlayer)
            {
                if (this.connectionToServer.isReady)
                {
                    Shoot();
                }
                else
                {
                    StartCoroutine(WaitForReady());
                }
            }
           
        }

        [Command]
        protected virtual void Shoot()
        {
            CreateProjectile();
        }

        protected IEnumerator WaitForReady()
        {
            while (!connectionToServer.isReady)
            {
                yield return new WaitForSeconds(0.25f);
            }
            CreateProjectile();
        }

        protected virtual void OnAltFire()
        {
            if (!LevelSettings.Instance.IsPaused)
            {
            
            }
        }

        [Server]
        protected virtual void CreateProjectile()
        {
            GameObject projectile = Instantiate(_projectile, _whereToSpawn.position, _referenceRotation.rotation);
            NetworkServer.Spawn(projectile);
        }
   
    }
}