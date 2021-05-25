using Core.Player;

namespace NonCore.Player.MegaMan
{
    [System.Serializable]
    public class ChargeableProjectile
    {
        public Projectile Projectile;
        public VFXCaller ProjectileVFXCaller;
        public int ChargeTime;
    }
}
