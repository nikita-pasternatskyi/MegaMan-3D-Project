using Core.Levels;
using UnityEngine;

namespace Core.Player
{
    abstract class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject _projectile;
        [SerializeField] private Transform _whereToSpawn;
        [SerializeField] private Transform _referenceRotation;
        [SerializeField] private float _maxAmmo;
        private float _currentAmmo;
        public virtual void Refill(float ammoToRefill)
        {
            _currentAmmo = _currentAmmo + ammoToRefill < _maxAmmo ? _currentAmmo + ammoToRefill : _maxAmmo;
            RefreshWeaponUI();
        }

        private void Awake()
        {
            _currentAmmo = _maxAmmo;
        }

        protected virtual void RefreshWeaponUI()
        { 
        
        }
        protected virtual void OnMainFire()
        {
            if (!LevelSettings.Instance.IsPaused)
            {
                Instantiate(_projectile, _whereToSpawn.position, _referenceRotation.rotation);
            }
        }
        protected virtual void OnAltFire()
        {
            if (!LevelSettings.Instance.IsPaused)
            {
            
            }
        }

   
    }
}
