using System;
using UnityEngine;

namespace Healthy
{
    public class HealthController : IHealth, IHealthEvents
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
            get => _humbleObject.Current;
            set
            {
                _history.Item2 = value;
                _humbleObject.Current = Mathf.Clamp(value, Min, Max);
                _history.Item1 = _humbleObject.Current;

                OnValueChanged();
            }
        }

        public bool CanHeal
        {
            get => _humbleObject.CanHeal;
            set => _humbleObject.CanHeal = value;
        }

        public bool CanTakeDamage
        {
            get => _humbleObject.CanTakeDamage;
            set => _humbleObject.CanTakeDamage = value;
        }

        public float Max
        {
            get => _humbleObject.Max;
            set => _humbleObject.Max = value;
        }

        public float Min
        {
            get => _humbleObject.Min;
            set => _humbleObject.Min = value;
        }

        private readonly IHealth _humbleObject;

        private (float, float) _history;

        public HealthController(IHealth humbleObject)
        {
            _humbleObject = humbleObject;
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