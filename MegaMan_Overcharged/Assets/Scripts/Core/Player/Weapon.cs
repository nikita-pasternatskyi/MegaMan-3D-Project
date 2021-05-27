namespace Core.Player
{
    public abstract class Weapon : RequiresInput
    {
        public abstract new void OnAlternateFire();

        public abstract new void OnMainFire();

        public abstract void Refill(int value);
    }

}
