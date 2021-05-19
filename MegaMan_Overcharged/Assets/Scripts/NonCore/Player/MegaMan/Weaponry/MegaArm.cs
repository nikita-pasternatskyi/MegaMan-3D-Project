using UnityEngine;
using Core.Player;
using Core.General;
using Core.Levels;

namespace NonCore.Player.MegaMan
{
    public class MegaArm : Weapon
    {
        [SerializeField] private Transform _whereToSpawn;
        [SerializeField] private Transform _referenceRotation;

        private GameObject _currentArmProjectile;

        public MegaArm(Transform whereToSpawn, Transform referenceRotation)
        {
            _whereToSpawn = whereToSpawn;
            _referenceRotation = referenceRotation;
        }

        public override void OnAlternateFire()
        {
            if (_currentArmProjectile == null && !LevelSettings.Instance.IsPaused)
            {
                _currentArmProjectile = ObjectSpawner.SpawnObject(AlternateFireProjectile, _whereToSpawn.position, _referenceRotation.rotation);
                _currentArmProjectile.GetComponent<MegaArmProjectile>().Initialize(_whereToSpawn);
            }
        }

    }
}
