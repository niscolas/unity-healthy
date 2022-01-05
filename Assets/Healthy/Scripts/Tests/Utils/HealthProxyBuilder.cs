namespace Healthy.Tests.Utils
{
    public class HealthProxyBuilder
    {
        private readonly HealthProxy _healthProxy = new HealthProxy(default, default);

        public HealthProxyBuilder WithCurrent(float value)
        {
            _healthProxy.Current = value;
            return this;
        }

        public HealthProxyBuilder WithMax(float value)
        {
            _healthProxy.Max = value;
            return this;
        }

        public HealthProxyBuilder WithMin(float value)
        {
            _healthProxy.Min = value;
            return this;
        }

        public HealthProxyBuilder WhichCanHeal(bool value = true)
        {
            _healthProxy.CanHeal = value;
            return this;
        }
        
        public HealthProxyBuilder WhichCanTakeDamage(bool value = true)
        {
            _healthProxy.CanTakeDamage = value;
            return this;
        }

        private HealthProxy Build()
        {
            return _healthProxy;
        }

        public static implicit operator HealthProxy(HealthProxyBuilder builder)
        {
            return builder.Build();
        }
    }
}