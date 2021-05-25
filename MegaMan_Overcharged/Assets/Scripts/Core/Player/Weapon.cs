namespace Core.Player
{

    public abstract class Weapon : RequiresInput
    {
        public virtual new void OnAlternateFire()
        {
            throw new System.NotImplementedException();
        }

        public virtual new void OnMainFire()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Refill(int value)
        {
            throw new System.NotImplementedException();
        }
    }

}
