using Core.General;
using Core.Levels;
using Core.Player;
using UnityEngine;

namespace NonCore.Player.MegaMan
{
    public class MegaArm : Weapon
    {
        [SerializeField] private Transform _whereToSpawn;
        [SerializeField] private Transform _referenceRotation;

        [SerializeField] private Projectile _alternateFireProjectile;
        private GameObject _currentArmProjectile;

        public override void OnMainFire()
        {
        }

        public override void Refill(int value)
        {
        }

        public override void OnAlternateFire()
        {
            if (_currentArmProjectile == null && !LevelSettings.Instance.IsPaused)
            {
                _currentArmProjectile = ObjectSpawner.SpawnObject(_alternateFireProjectile.gameObject, _whereToSpawn.position, _referenceRotation.rotation);
                _currentArmProjectile.GetComponent<MegaArmProjectile>().Initialize(_whereToSpawn);
            }
        }

    }
}
