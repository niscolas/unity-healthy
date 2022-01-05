using System;

namespace Healthy
{
    public interface IHealth
    {
        bool CanHeal { get; set; }

        bool CanTakeDamage { get; set; }

        float Current { get; set; }

        float Max { get; set; }

        float Min { get; set; }

        void Heal(
            float healValue,
            object instigator = null,
            Action<(float, float)> healedWithHistoryCallback = null,
            Action revivedCallback = null);

        void TakeDamage(
            float damageValue,
            object instigator = null,
            Action<(float, float)> damageTakenWithHistoryCallback = null,
            Action deathCallback = null);
    }
}