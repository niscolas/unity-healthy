namespace niscolas.Healthy
{
    public interface IHealable
    {
        void Heal(float rawValue);
        void HealRelative(float ratio);
    }
}