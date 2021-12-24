using FluentAssertions;
using Healthy.Tests.Utils;
using NUnit.Framework;

namespace Healthy.Tests.Editor
{
    [TestFixture]
    public class HealTests
    {
        [TestCase(50, 100, 0.2f, 70)]
        [TestCase(25, 50, 0.2f, 35)]
        public void HealRelative_DynamicInitial_DynamicMax_DynamicHealRatio_CurrentShouldEqualExpected(
            float initial, float max, float healRatio, float expected)
        {
            IHealth health = A.HealthMock
                .WithCurrent(initial)
                .WithMax(max)
                .Build();
            HealthController healthController = new HealthController(health);

            healthController.HealRelative(healRatio);

            health.Current.Should().Be(expected);
        }

        [TestCase(50, 100, 20, 70)]
        [TestCase(25, 50, 20, 45)]
        public void HealRaw_DynamicInitial_DynamicMax_DynamicHealRatio_CurrentShouldEqualExpected(
            float initial, float max, float rawHeal, float expected)
        {
            IHealth health = A.HealthMock
                .WithCurrent(initial)
                .WithMax(max)
                .Build();
            HealthController healthController = new HealthController(health);

            healthController.HealRaw(rawHeal);

            health.Current.Should().Be(expected);
        }

        [TestCase(30, 60, 1.1f)]
        [TestCase(20, 80, 2)]
        [TestCase(10, 200, 1.3f)]
        public void HealRelative_DynamicInitial_DynamicMax_DynamicHealRatio_CurrentShouldEqualMax(
            float initial, float max, float healRatio)
        {
            IHealth health = A.HealthMock
                .WithCurrent(initial)
                .WithMax(max)
                .Build();
            HealthController healthController = new HealthController(health);

            healthController.HealRelative(healRatio);

            health.Current.Should().Be(max);
        }
    }
}