using Core.General;
using Core.Player;
using System.Threading.Tasks;
using UnityEngine;

namespace NonCore.Player.MegaMan
{

    public class MegaBuster : Weapon
    {
        [SerializeField] private Transform _whereToSpawn;
        [SerializeField] private Transform _referenceRotation;
        private GameObject _firstStepPrefab;
        private GameObject _secondStepPrefab;
        private GameObject _thirdStepPrefab;

        private float _firstStep;
        private float _secondStep;
        private float _thirdStep;

        public override void OnAlternateFire()
        {            
        }

        public override void OnMainFire()
        {
            //ChargeBuster();
            ObjectSpawner.SpawnObject(MainFireProjectile, _whereToSpawn.position, _referenceRotation.rotation);
        }

        public override void Refill(int refillValue)
        {
            base.Refill(refillValue);
        }


        private async void ChargeBuster()
        {
            await Timer(_firstStep);
            
            await Timer(_secondStep);
            
            await Timer(_thirdStep);
            
        }

        public static async Task<bool> Timer(float duration)
        {
            while (duration > 0)
            {
                duration-= Time.fixedDeltaTime;
                await Task.Yield();
            }
            if (duration <= 0)
                return true;
            return false;
        }

    }
}
