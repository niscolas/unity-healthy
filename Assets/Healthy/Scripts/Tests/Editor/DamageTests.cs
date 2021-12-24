using FluentAssertions;
using Healthy.Tests.Utils;
using NUnit.Framework;

namespace Healthy.Tests.Editor
{
    [TestFixture]
    public class DamageTests
    {
        [TestCase(100, 0.2f, 80)]
        [TestCase(50, 0.2f, 40)]
        public void RelativeDamage_DynamicInitialHealth_DynamicDamageRatio_CurrentShouldEqualExpected(
            float initial, float damageRatio, float expected)
        {
            IHealth health = A.HealthMock
                .WithCurrent(initial)
                .WithMax(initial)
                .Build();
            HealthController healthController = new HealthController(health);

            healthController.TakeRelativeDamage(damageRatio);

            health.Current.Should().Be(expected);
        }

        [TestCase(10, 3, 7)]
        [TestCase(20, 3, 17)]
        public void RawDamage_DynamicInitialHealth_DynamicDamageValue_CurrentShouldEqualExpected(
            float initial, float rawDamage, float expected)
        {
            IHealth health = A.HealthMock
                .WithCurrent(initial)
                .WithMax(initial)
                .Build();
            HealthController healthController = new HealthController(health);

            healthController.TakeRawDamage(rawDamage);

            health.Current.Should().Be(expected);
        }

        [TestCase(100, 1)]
        [TestCase(50, 2)]
        [TestCase(25, 1.5f)]
        public void RelativeDamage_DynamicInitialHealth_DynamicDamageRatio_CurrentShouldEqualMin(
            float initial, float damageRatio, float min = 0)
        {
            IHealth health = A.HealthMock
                .WithCurrent(initial)
                .WithMax(initial)
                .WithMin(min)
                .Build();
            HealthController healthController = new HealthController(health);

            healthController.TakeRelativeDamage(damageRatio);

            health.Current.Should().Be(min);
        }
    }
}