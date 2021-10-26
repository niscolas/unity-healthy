namespace niscolas.Healthy
{
    public interface IDamageable
    {
        void TakeDamage(float rawValue);
        void TakeRelativeDamage(float ratio);
    }
}