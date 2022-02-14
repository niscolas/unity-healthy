using System;

namespace Healthy
{
    public interface IHealth : IHealthData
    {
        event Action<float> DamageTaken;
        event Action<(float, float)> DamageTakenWithHistory;
        event Action Died;
        event Action<float> Healed;
        event Action<(float, float)> HealedWithHistory;
        event Action Revived;
        event Action<float> ValueChanged;
        event Action<(float, float)> ValueChangedWithHistory;

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