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
        public void TakeDamage_DynamicInitialHealth_DynamicDamageRatio_CurrentShouldEqualExpected(
            float initial, float damageRatio, float expected)
        {
            HealthController healthController = A.HealthController
                .WithMax(initial)
                .WithCurrent(initial)
                .WhichCanTakeDamage();

            healthController.TakeDamage(initial * damageRatio);

            healthController.Current.Should().Be(expected);
        }

        [TestCase(10, 3, 7)]
        [TestCase(20, 3, 17)]
        public void TakeDamage_DynamicInitialHealth_DynamicDamageValue_CurrentShouldEqualExpected(
            float initial, float rawDamage, float expected)
        {
            HealthController healthController = A.HealthController
                .WithMax(initial)
                .WithCurrent(initial)
                .WhichCanTakeDamage();

            healthController.TakeDamage(rawDamage);

            healthController.Current.Should().Be(expected);
        }

        [TestCase(100, 1)]
        [TestCase(50, 2)]
        [TestCase(25, 1.5f)]
        public void TakeDamage_DynamicInitialHealth_DynamicDamageRatio_CurrentShouldEqualMin(
            float initial, float damageRatio, float min = 0)
        {
            HealthController healthController = A.HealthController
                .WithMax(initial)
                .WithMin(min)
                .WithCurrent(initial)
                .WhichCanTakeDamage();

            healthController.TakeDamage(initial * damageRatio);

            healthController.Current.Should().Be(min);
        }
    }
}