namespace Healthy.Tests.Utils
{
    public class HealthBuilder
    {
        private readonly Health _health = new Health();

        public HealthBuilder WithCurrent(float value)
        {
            _health.Current = value;
            return this;
        }

        public HealthBuilder WithMax(float value)
        {
            _health.Max = value;
            return this;
        }

        public HealthBuilder WithMin(float value)
        {
            _health.Min = value;
            return this;
        }

        public HealthBuilder WhichCanHeal(bool value = true)
        {
            _health.CanHeal = value;
            return this;
        }
        
        public HealthBuilder WhichCanTakeDamage(bool value = true)
        {
            _health.CanTakeDamage = value;
            return this;
        }

        private Health Build()
        {
            return _health;
        }

        public static implicit operator Health(HealthBuilder builder)
        {
            return builder.Build();
        }
    }
}