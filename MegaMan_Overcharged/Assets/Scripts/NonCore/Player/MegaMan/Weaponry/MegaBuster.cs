using Core.General;
using Core.Player;
using UnityEngine;

namespace NonCore.Player.MegaMan
{

    public class MegaBuster : Weapon
    {
        
        private Transform _whereToSpawn;
        private Transform _referenceRotation;
        public override void AlternateFire()
        {            
        }

        public override void MainFire()
        {
            ObjectSpawner.SpawnObject(MainFireProjectile, _whereToSpawn.position, _referenceRotation.rotation);
        }

        public override void Refill(int refillValue)
        {
            base.Refill(refillValue);
        }

    }
}
