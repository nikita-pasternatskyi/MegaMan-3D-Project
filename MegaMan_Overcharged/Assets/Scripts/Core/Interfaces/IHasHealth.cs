namespace Core.Interfaces
{
    public interface IHasHealth
    {
        public void TakeDamage(int damage);
        public void Heal(int healCount);
        
    }
}
