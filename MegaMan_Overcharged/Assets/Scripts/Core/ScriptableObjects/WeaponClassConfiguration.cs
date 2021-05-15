using Core.Player;
using UnityEngine;

namespace Core.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WeaponConfigurations", menuName = "ScriptableObjects/Weapon", order = 1)]
    public class WeaponClassConfiguration : ScriptableObject
    {
        public Weapon Weapon;
        public GameObject MainFireProjectile;
        public GameObject AlternateFireProjectile;
        public float MaximumAmmo;
    }
}
