using Core.General;
using Core.Player;
using UnityEngine;

namespace NonCore.Player.MegaMan
{

    public class MegaBuster : Weapon
    {
        [SerializeField] private Transform _whereToSpawn;
        [SerializeField] private Transform _referenceRotation;
        public override void OnAlternateFire()
        {            
        }

        public override void OnMainFire()
        {
            ObjectSpawner.SpawnObject(MainFireProjectile, _whereToSpawn.position, _referenceRotation.rotation);
        }

        public override void Refill(int refillValue)
        {
            base.Refill(refillValue);
        }

    }
}
