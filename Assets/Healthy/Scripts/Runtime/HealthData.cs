namespace Healthy
{
    public class HealthData : IHealthData
    {
        public float Current { get; set; }

        public bool CanTakeDamage { get; set; }

        public bool CanHeal { get; set; }

        public float Max { get; set; }

        public float Min { get; set; }

        public HealthData(
            float current,
            float max,
            float min = default,
            bool canTakeDamage = true,
            bool canHeal = true)
        {
            Current = current;
            CanTakeDamage = canTakeDamage;
            CanHeal = canHeal;
            Max = max;
            Min = min;
        }
    }
}