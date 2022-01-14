namespace Healthy.Tests.Utils
{
    public class HealthControllerBuilder
    {
        private readonly HealthData _data = new HealthData(default, default);

        public HealthControllerBuilder WithCurrent(float value)
        {
            _data.Current = value;
            return this;
        }

        public HealthControllerBuilder WithMax(float value)
        {
            _data.Max = value;
            return this;
        }

        public HealthControllerBuilder WithMin(float value)
        {
            _data.Min = value;
            return this;
        }

        public HealthControllerBuilder WhichCanHeal(bool value = true)
        {
            _data.CanHeal = value;
            return this;
        }

        public HealthControllerBuilder WhichCanTakeDamage(bool value = true)
        {
            _data.CanTakeDamage = value;
            return this;
        }

        private HealthController Build()
        {
            return new HealthController(_data);
        }

        public static implicit operator HealthController(HealthControllerBuilder builder)
        {
            return builder.Build();
        }
    }
}