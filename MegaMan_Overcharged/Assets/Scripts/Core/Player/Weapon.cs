using Core.Levels;
using Core.ScriptableObjects;
using UnityEngine;

namespace Core.Player
{
    [System.Serializable]
    public abstract class Weapon
    {
        private WeaponClassConfiguration _weaponClassConfiguration;
        public GameObject MainFireProjectile { get => _weaponClassConfiguration.MainFireProjectile; private set { } }
        public GameObject AlternateFireProjectile { get => _weaponClassConfiguration.AlternateFireProjectile; private set { } }

        public virtual void Refill(int refillValue)
        { }

        public virtual void MainFire()
        { }

        public virtual void AlternateFire()
        { }
    }

   
}
