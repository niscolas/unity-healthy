using NSubstitute;

namespace Healthy.Tests.Utils
{
    public class HealthMockBuilder
    {
        private readonly IHealth _health = Substitute.For<IHealth>();

        public HealthMockBuilder WithCurrent(float value)
        {
            _health.Current.Returns(value);
            return this;
        }

        public HealthMockBuilder WithMax(float value)
        {
            _health.Max.Returns(value);
            return this;
        }

        public HealthMockBuilder WithMin(float value)
        {
            _health.Min.Returns(value);
            return this;
        }

        public HealthMockBuilder Invulnerable(bool value = true)
        {
            _health.IsInvulnerable.Returns(value);
            return this;
        }

        public IHealth Build()
        {
            return _health;
        }
    }
}