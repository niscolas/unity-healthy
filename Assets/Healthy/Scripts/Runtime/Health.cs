using System;
using UnityEngine;

namespace Healthy
{
    public class Health : IHealth, IHealthEvents
    {
        public event Action<float> DamageTaken;
        public event Action<(float, float)> DamageTakenWithHistory;
        public event Action Died;
        public event Action<float> Healed;
        public event Action<(float, float)> HealedWithHistory;
        public event Action Revived;
        public event Action<float> ValueChanged;
        public event Action<(float, float)> ValueChangedWithHistory;

        public float Current
        {
            get => _data.Current;
            set
            {
                _history.Item2 = value;
                _data.Current = Mathf.Clamp(value, Min, Max);
                _history.Item1 = _data.Current;

                OnValueChanged();
            }
        }

        float IHealthData.Current { get; set; }

        public bool CanHeal
        {
            get => _data.CanHeal;
            set => _data.CanHeal = value;
        }

        bool IHealthData.CanHeal { get; set; }

        public bool CanTakeDamage
        {
            get => _data.CanTakeDamage;
            set => _data.CanTakeDamage = value;
        }

        bool IHealthData.CanTakeDamage { get; set; }

        public float Max
        {
            get => _data.Max;
            set => _data.Max = value;
        }

        float IHealthData.Max { get; set; }

        public float Min
        {
            get => _data.Min;
            set => _data.Min = value;
        }

        float IHealthData.Min { get; set; }

        private readonly IHealthData _data;

        private (float, float) _history;

        public Health()
        {
            _data = this;
        }

        public Health(IHealthData healthData)
        {
            _data = healthData;
        }

        public Health(float current, float max, float min = 0, bool canHeal = true, bool canTakeDamage = true)
        {
            _data = this;
            Max = max;
            Min = min;
            CanHeal = canHeal;
            CanTakeDamage = canTakeDamage;
            Current = current;
        }

        public void Heal(
            float healValue,
            object instigator = null,
            Action<(float, float)> healedWithHistoryCallback = null,
            Action revivedCallback = null)
        {
            if (!CheckCanHeal(healValue))
            {
                return;
            }

            Current += healValue;
            OnHealed(healedWithHistoryCallback);

            if (CheckIsBeingRevived())
            {
                OnRevived(revivedCallback);
            }
        }

        public void TakeDamage(
            float damageValue,
            object instigator = null,
            Action<(float, float)> damageTakenWithHistoryCallback = null,
            Action deathCallback = null)
        {
            if (!CheckCanTakeDamage(damageValue))
            {
                return;
            }

            Current -= damageValue;
            OnDamaged(damageTakenWithHistoryCallback);

            if (Current <= 0)
            {
                OnDeath(deathCallback);
            }
        }

        private static bool CheckIsSignificantHealthDelta(float healthDelta)
        {
            return !Mathf.Approximately(healthDelta, 0);
        }

        private bool CheckCanHeal(float healValue)
        {
            return CanHeal && CheckIsSignificantHealthDelta(healValue);
        }

        private bool CheckCanTakeDamage(float damageValue)
        {
            return CanTakeDamage && CheckIsSignificantHealthDelta(damageValue);
        }

        private bool CheckIsBeingRevived()
        {
            return Current <= 0;
        }

        private void OnDamaged(Action<(float, float)> callback = null)
        {
            callback?.Invoke(_history);
            DamageTaken?.Invoke(_history.Item1);
            DamageTakenWithHistory?.Invoke(_history);
        }

        private void OnDeath(Action callback = null)
        {
            callback?.Invoke();
            Died?.Invoke();
        }

        private void OnHealed(Action<(float, float)> callback = null)
        {
            callback?.Invoke(_history);
            Healed?.Invoke(_history.Item1);
            HealedWithHistory?.Invoke(_history);
        }

        private void OnRevived(Action callback = null)
        {
            callback?.Invoke();
            Revived?.Invoke();
        }

        private void OnValueChanged()
        {
            ValueChanged?.Invoke(_history.Item1);
            ValueChangedWithHistory?.Invoke(_history);
        }
    }
}