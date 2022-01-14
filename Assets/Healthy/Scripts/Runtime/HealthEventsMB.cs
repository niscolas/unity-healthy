using System;
using niscolas.UnityUtils.Core;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Events;

namespace Healthy
{
    [AddComponentMenu(Constants.AddComponentMenuPrefix + "Health Events")]
    public class HealthEventsMB : CachedMB
    {
        [SerializeField]
        private HealthMB _health;

        [Header(HeaderTitles.Events)]
        [SerializeField]
        private UnityEvent<float> _valueChanged;

        [SerializeField]
        private UnityEvent<FloatPair> _valueChangedWithHistory;

        [SerializeField]
        private UnityEvent<float> _damageTaken;

        [SerializeField]
        private UnityEvent<FloatPair> _damageTakenWithHistory;

        [SerializeField]
        private UnityEvent<float> _healed;

        [SerializeField]
        private UnityEvent<FloatPair> _healedWithHistory;

        [SerializeField]
        private UnityEvent _revived;

        [SerializeField]
        private UnityEvent _died;

        private void Start()
        {
            base.Awake();

            _health.DamageTaken += OnDamageTaken;
            _health.DamageTakenWithHistory += OnDamageTakenWithHistory;
            _health.Died += OnDied;
            _health.Healed += OnHealed;
            _health.HealedWithHistory += OnHealedWithHistory;
            _health.Revived += OnRevived;
            _health.ValueChanged += OnValueChanged;
            _health.ValueChangedWithHistory += OnValueChangedWithHistory;
        }

        private void OnDestroy()
        {
            _health.DamageTaken -= OnDamageTaken;
            _health.DamageTakenWithHistory -= OnDamageTakenWithHistory;
            _health.Died -= OnDied;
            _health.Healed -= OnHealed;
            _health.HealedWithHistory -= OnHealedWithHistory;
            _health.Revived -= OnRevived;
            _health.ValueChanged -= OnValueChanged;
            _health.ValueChangedWithHistory -= OnValueChangedWithHistory;
        }

        private void OnDamageTaken(float value)
        {
            _damageTaken?.Invoke(value);
        }

        private void OnDamageTakenWithHistory(ValueTuple<float, float> history)
        {
            _damageTakenWithHistory?.Invoke(new FloatPair
            {
                Item1 = history.Item1,
                Item2 = history.Item2,
            });
        }

        private void OnDied()
        {
            _died?.Invoke();
        }

        private void OnHealed(float value)
        {
            _healed?.Invoke(value);
        }

        private void OnHealedWithHistory((float, float) history)
        {
            _healedWithHistory?.Invoke(new FloatPair
            {
                Item1 = history.Item1,
                Item2 = history.Item2
            });
        }

        private void OnRevived()
        {
            _revived?.Invoke();
        }

        private void OnValueChanged(float value)
        {
            _valueChanged?.Invoke(value);
        }

        private void OnValueChangedWithHistory((float, float) history)
        {
            _valueChangedWithHistory?.Invoke(new FloatPair
            {
                Item1 = history.Item1,
                Item2 = history.Item2
            });
        }
    }
}