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
        public void Heal_DynamicInitial_DynamicMax_DynamicHealRatio_CurrentShouldEqualExpected(
            float initial, float max, float healRatio, float expected)
        {
            Health health = A.Health
                .WithMax(max)
                .WithCurrent(initial)
                .WhichCanHeal();

            health.Heal(max * healRatio);

            health.Current.Should().Be(expected);
        }

        [TestCase(50, 100, 20, 70)]
        [TestCase(25, 50, 20, 45)]
        public void Heal_DynamicInitial_DynamicMax_DynamicHeal_CurrentShouldEqualExpected(
            float initial, float max, float rawHeal, float expected)
        {
            Health health = A.Health
                .WithMax(max)
                .WithCurrent(initial)
                .WhichCanHeal();

            health.Heal(rawHeal);

            health.Current.Should().Be(expected);
        }

        [TestCase(30, 60, 1.1f)]
        [TestCase(20, 80, 2)]
        [TestCase(10, 200, 1.3f)]
        public void Heal_DynamicInitial_DynamicMax_DynamicHealRatio_CurrentShouldEqualMax(
            float initial, float max, float healRatio)
        {
            Health health = A.Health
                .WithMax(max)
                .WithCurrent(initial)
                .WhichCanHeal();

            health.Heal(max * healRatio);

            health.Current.Should().Be(max);
        }
    }
}