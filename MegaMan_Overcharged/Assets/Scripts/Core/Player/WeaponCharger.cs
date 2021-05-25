using Core.General;
using NonCore.Player.MegaMan;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Player
{
    [System.Serializable]
    public class WeaponCharger
    {
        public event Action<int> FullyChargedStep;
        private MonoBehaviour _parentMonoBehaviour;
        [SerializeField] private ChargeableProjectile[] _ChargeableProjectiles;

        [SerializeField] private int _startAutomaticCharging = 0;
        [SerializeField] private int _endAutomaticCharging = 1;
        [SerializeField] private bool _automaticCharging = true;

        private Coroutine _timer;
        private int[] _timesForManualTimer;
        private int[] _timesForAutomaticCharge;

        private Projectile _currentProjectile;
        private VFXCaller _currentVFXCaller;

        public bool pressed;

        private Transform _whereToSpawn;
        private Transform _referenceRotation;

        public void Init(in Transform whereToSpawn, in Transform referenceRotation, in MonoBehaviour targetMonoBehaviour)
        {
            _parentMonoBehaviour = targetMonoBehaviour;
            _whereToSpawn = whereToSpawn;
            _referenceRotation = referenceRotation;

            InitializeTimers();
            _currentProjectile = _ChargeableProjectiles[_startAutomaticCharging].Projectile;
            _currentVFXCaller = _ChargeableProjectiles[_endAutomaticCharging].ProjectileVFXCaller;

            CreateAutomaticChargeTimer();
        }

        public void FireShot(in VFXCaller muzzleFlash)
        {
            pressed = pressed ? false : true;
            _parentMonoBehaviour.StopCoroutine(_timer);
            _timer = null;
            if (!pressed)
            {
                if (_currentProjectile != _ChargeableProjectiles[0].Projectile)
                    SpawnShot(muzzleFlash);

                CreateAutomaticChargeTimer();
            }
            if (pressed)
            {
                SpawnShot(muzzleFlash);
                ChargeWeapon();
            }
            _currentVFXCaller.DisableVFX();
        }

        private void SpawnShot(in VFXCaller muzzleFlash)
        {
            muzzleFlash.EnableVFX();
            ObjectSpawner.SpawnObject(_currentProjectile.gameObject, _whereToSpawn.position, _referenceRotation.rotation);
        }
        private void ChargeWeapon() => _timer = Timer.CreateTimer(_parentMonoBehaviour, ChargeCallback, _timesForManualTimer);
        private void ChargeCallback(int chargeShotIndex)
        {
            FullyChargedStep?.Invoke(chargeShotIndex);
            _currentVFXCaller?.DisableVFX();
            _currentProjectile = _ChargeableProjectiles[chargeShotIndex].Projectile;
            _currentVFXCaller = _ChargeableProjectiles[chargeShotIndex].ProjectileVFXCaller;
            _currentVFXCaller.EnableVFX();
        }
        private void InitializeTimers()
        {
            _timesForAutomaticCharge = new int[_endAutomaticCharging+1];
            for (int i = 0; i < _endAutomaticCharging+1; i++)
            {
                _timesForAutomaticCharge[i] =_ChargeableProjectiles[i].ChargeTime;
            }

            _timesForManualTimer = new int[_ChargeableProjectiles.Length];
            for (int i = 0; i < _ChargeableProjectiles.Length; i++)
            {
                _timesForManualTimer[i] = _ChargeableProjectiles[i].ChargeTime;
            }

        }
        private void CreateAutomaticChargeTimer()
        {
            if (_automaticCharging)
            {
                _timer = Timer.CreateTimer(_parentMonoBehaviour, ChargeCallback, _timesForAutomaticCharge);
            }
        }
    }
}
