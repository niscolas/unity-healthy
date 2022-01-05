using System;

namespace Healthy
{
    public class HealthProxy : IHealth, IHealthEvents
    {
        public event Action<float> DamageTaken;
        public event Action<(float, float)> DamageTakenWithHistory;
        public event Action Died;
        public event Action<float> Healed;
        public event Action<(float, float)> HealedWithHistory;
        public event Action Revived;
        public event Action<float> ValueChanged;
        public event Action<(float, float)> ValueChangedWithHistory;

        public float Current { get; set; }

        public bool CanTakeDamage { get; set; }

        public bool CanHeal { get; set; }

        public float Max { get; set; }

        public float Min { get; set; }

        private readonly HealthController _controller;

        public HealthProxy(
            float current,
            float max,
            float min = default,
            bool canTakeDamage = true,
            bool canHeal = true)
        {
            _controller = new HealthController(this);

            Current = current;
            CanTakeDamage = canTakeDamage;
            CanHeal = canHeal;
            Max = max;
            Min = min;
        }

        public void Heal(
            float healValue,
            object instigator = null,
            Action<(float, float)> healedWithHistoryCallback = null,
            Action revivedCallback = null)
        {
            _controller.Heal(healValue, instigator, healedWithHistoryCallback, revivedCallback);
        }

        public void TakeDamage(
            float damageValue,
            object instigator = null,
            Action<(float, float)> damageTakenWithHistoryCallback = null,
            Action deathCallback = null)
        {
            _controller.TakeDamage(damageValue, instigator, damageTakenWithHistoryCallback, deathCallback);
        }
    }
}