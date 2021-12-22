using System;
using UnityEngine;

namespace niscolas.Healthy
{
    public class HealthController
    {
        private readonly IHealth _health;
        private readonly Action<float> _setCurrentHealth;

        public HealthController(
            IHealth health,
            Action<float> setCurrentHealth)
        {
            _health = health;
            _setCurrentHealth = setCurrentHealth;
        }

        public void TakeRelativeDamage(
            float ratio,
            Action<float> damageTakenCallback = null,
            Action deathCallback = null)
        {
            float rawDamage = _health.Max * ratio;
            TakeRawDamage(rawDamage, damageTakenCallback, deathCallback);
        }

        public void TakeRawDamage(
            float value,
            Action<float> damageTakenCallback = null,
            Action deathCallback = null)
        {
            value = Mathf.Abs(value);

            if (_health.IsInvulnerable || !CheckIsSignificantHealthDelta(value))
            {
                return;
            }

            _setCurrentHealth(_health.Current - value);

            damageTakenCallback?.Invoke(_health.Current);

            if (_health.Current > 0)
            {
                return;
            }

            deathCallback?.Invoke();
        }

        public void HealRelative(
            float ratio,
            Action<float> healedCallback = null,
            Action revivedCallback = null)
        {
            float rawHeal = _health.Max * ratio;
            HealRaw(rawHeal, healedCallback, revivedCallback);
        }

        public void HealRaw(
            float value,
            Action<float> healedCallback = null,
            Action revivedCallback = null)
        {
            value = Mathf.Abs(value);

            if (!CheckIsSignificantHealthDelta(value))
            {
                return;
            }

            _setCurrentHealth(
                Mathf.Min(
                    _health.Current + value,
                    _health.Max));

            healedCallback?.Invoke(_health.Current);

            if (CheckIsBeingRevived())
            {
                revivedCallback?.Invoke();
            }
        }

        private static bool CheckIsSignificantHealthDelta(float healthDelta)
        {
            return !Mathf.Approximately(healthDelta, 0);
        }

        private bool CheckIsBeingRevived()
        {
            return _health.Current <= 0;
        }
    }
}