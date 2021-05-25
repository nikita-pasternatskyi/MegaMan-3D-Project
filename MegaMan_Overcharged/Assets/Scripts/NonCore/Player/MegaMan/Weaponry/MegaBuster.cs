using Core.Player;
using UnityEngine;

namespace NonCore.Player.MegaMan
{
    public class MegaBuster : Weapon
    {
        [SerializeField] private Transform _whereToSpawn;
        [SerializeField] private Transform _spawnRotation;
        [SerializeField] private VFXCaller _muzzleFlash;

        [SerializeField] private WeaponCharger _mainFireCharger;

        private void Start()
        {
            _mainFireCharger.Init(_whereToSpawn, _spawnRotation, this);
        }

        public override void OnAlternateFire()
        {
        }
        public override void OnMainFire()
        {
            _mainFireCharger.FireShot(_muzzleFlash);
        }
        public override void Refill(int value)
        {
        }

    }
}
