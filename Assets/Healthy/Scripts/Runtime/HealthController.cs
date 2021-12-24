using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Healthy
{
    public class HealthController
    {
        private IHealth _humbleHealth;
        private FloatPair _history = new FloatPair();

        public HealthController(IHealth health)
        {
            _humbleHealth = health;
        }

        public void HealRelative(
            float ratio,
            Action<FloatPair> healedCallback = null,
            Action revivedCallback = null)
        {
            float rawHeal = _humbleHealth.Max * ratio;
            HealRaw(rawHeal, healedCallback, revivedCallback);
        }

        public void HealRaw(
            float value,
            Action<FloatPair> healedCallback = null,
            Action revivedCallback = null)
        {
            value = Mathf.Abs(value);

            if (!CheckIsSignificantHealthDelta(value))
            {
                return;
            }

            SetCurrent(_humbleHealth.Current + value);
            healedCallback?.Invoke(_history);

            if (CheckIsBeingRevived())
            {
                revivedCallback?.Invoke();
            }
        }

        public void TakeRelativeDamage(
            float ratio,
            Action<FloatPair> damageTakenCallback = null,
            Action deathCallback = null)
        {
            float rawDamage = _humbleHealth.Max * ratio;
            TakeRawDamage(rawDamage, damageTakenCallback, deathCallback);
        }

        public void TakeRawDamage(
            float value,
            Action<FloatPair> damageTakenCallback = null,
            Action deathCallback = null)
        {
            value = Mathf.Abs(value);

            if (_humbleHealth.IsInvulnerable || !CheckIsSignificantHealthDelta(value))
            {
                return;
            }

            SetCurrent(_humbleHealth.Current - value);
            OnDamaged(damageTakenCallback);

            if (_humbleHealth.Current > 0)
            {
                return;
            }

            OnDeath(deathCallback);
        }

        private static bool CheckIsSignificantHealthDelta(float healthDelta)
        {
            return !Mathf.Approximately(healthDelta, 0);
        }

        private static void OnDeath(Action callback)
        {
            callback?.Invoke();
        }

        private bool CheckIsBeingRevived()
        {
            return _humbleHealth.Current <= 0;
        }

        private void OnDamaged(Action<FloatPair> callback)
        {
            callback?.Invoke(_history);
        }

        private void SetCurrent(float value)
        {
            _history.Item2 = value;
            _humbleHealth.Current = Mathf.Clamp(value, _humbleHealth.Min, _humbleHealth.Max);
            _history.Item1 = _humbleHealth.Current;
        }
    }
}