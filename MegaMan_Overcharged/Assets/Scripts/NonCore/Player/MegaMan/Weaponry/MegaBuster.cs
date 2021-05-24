using Core.General;
using Core.Player;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace NonCore.Player.MegaMan
{


    public class MegaBuster : Weapon
    {
        [SerializeField] private Projectile[] _projectiles;
        [SerializeField] private VFXCaller[] _projectileVfxCallers;

        [SerializeField] private Transform _whereToSpawn;
        [SerializeField] private Transform _referenceRotation;

        [SerializeField] private VFXCaller _muzzleFlash;

        [SerializeField] private int _firstStep;
        [SerializeField] private int _secondStep;
        [SerializeField] private int _thirdStep;


        private Coroutine chargeTimer;
        private Projectile _currentProjectile;
        private VFXCaller _currentVFXCaller;
        private bool pressed = false;

        private void Start()
        {
            _currentProjectile = _projectiles[0];
        }


        public override void OnAlternateFire()
        {            
        }

        public override void OnMainFire()
        {
            pressed = pressed ? false : true;

            if (!pressed && _currentProjectile != _projectiles[0])
            {
                _muzzleFlash.EnableVFX();
                ObjectSpawner.SpawnObject(_currentProjectile.gameObject, _whereToSpawn.position, _referenceRotation.rotation);
                _currentProjectile = _projectiles[0];
            }

            if(chargeTimer != null)
                StopCoroutine(chargeTimer);
            _currentVFXCaller.DisableVFX();

            if (pressed)
            {
                _muzzleFlash.EnableVFX();
                ObjectSpawner.SpawnObject(_currentProjectile.gameObject, _whereToSpawn.position, _referenceRotation.rotation);
                ChargeBuster();
            }
        }

        private void ChargeBuster()
        {
            chargeTimer = Timer.StartTimer(this, ChargeCallback, _firstStep, _secondStep, _thirdStep, _thirdStep + 10);
        }

        private void ChargeCallback(int chargeShotIndex)
        {
            if(_currentVFXCaller != null)
                _currentVFXCaller.DisableVFX();
            _currentProjectile = _projectiles[chargeShotIndex];
            _currentVFXCaller = _projectileVfxCallers[chargeShotIndex];
            _currentVFXCaller.EnableVFX();
        }

        public override void Refill(int refillValue)
        {
            base.Refill(refillValue);
        }


    }
}
