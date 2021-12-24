namespace Healthy
{
    public interface IHealth
    {
        float Current { get; set; }
        bool IsInvulnerable { get; }
        float Max { get; }
        float Min { get; }

        public void HealRelative(float ratio);
        public void HealRaw(float value);
        public void TakeRelativeDamage(float ratio);
        public void TakeRawDamage(float value);
    }
}