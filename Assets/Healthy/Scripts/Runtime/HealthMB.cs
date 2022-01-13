using System;
using niscolas.UnityUtils.Core;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Events;

namespace Healthy
{
    [AddComponentMenu(Constants.AddComponentMenuPrefix + "Health")]
    public class HealthMB : CachedMB, IHealth, IHealthEvents
    {
        private const string EventsLabel = "Events";

        [SerializeField]
        private FloatReference _current = new FloatReference(100);

        [SerializeField]
        private FloatReference _max = new FloatReference(100);

        [SerializeField]
        private FloatReference _min = new FloatReference(0);

        [SerializeField]
        private BoolReference _canHeal = new BoolReference(true);

        [SerializeField]
        private BoolReference _canTakeDamage = new BoolReference(true);

        [FoldoutGroup(EventsLabel)]
        [SerializeField]
        private UnityEvent<float> _valueChanged;

        [FoldoutGroup(EventsLabel)]
        [SerializeField]
        private UnityEvent<FloatPair> _valueChangedWithHistory;

        [FoldoutGroup(EventsLabel)]
        [SerializeField]
        private UnityEvent<float> _damageTaken;

        [FoldoutGroup(EventsLabel)]
        [SerializeField]
        private UnityEvent<FloatPair> _damageTakenWithHistory;

        [FoldoutGroup(EventsLabel)]
        [SerializeField]
        private UnityEvent<float> _healed;

        [FoldoutGroup(EventsLabel)]
        [SerializeField]
        private UnityEvent<FloatPair> _healedWithHistory;

        [FoldoutGroup(EventsLabel)]
        [SerializeField]
        private UnityEvent _revived;

        [FoldoutGroup(EventsLabel)]
        [SerializeField]
        private UnityEvent _died;

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
            get => _current.Value;
            set => _current.Value = value;
        }

        public bool CanTakeDamage
        {
            get => _canTakeDamage.Value;
            set => _canTakeDamage.Value = value;
        }

        public bool CanHeal
        {
            get => _canHeal.Value;
            set => _canHeal.Value = value;
        }

        public float Max
        {
            get => _max.Value;
            set => _max.Value = value;
        }

        public float Min
        {
            get => _min.Value;
            set => _min.Value = value;
        }

        private HealthController _healthController;

        protected override void Awake()
        {
            base.Awake();

            _healthController = new HealthController(this);

            _healthController.DamageTaken += OnDamageTaken;
            _healthController.DamageTakenWithHistory += OnDamageTakenWithHistory;
            _healthController.Died += OnDied;
            _healthController.Healed += OnHealed;
            _healthController.HealedWithHistory += OnHealedWithHistory;
            _healthController.Revived += OnRevived;
            _healthController.ValueChanged += OnValueChanged;
            _healthController.ValueChangedWithHistory += OnValueChangedWithHistory;
        }

        private void OnDestroy()
        {
            _healthController.DamageTaken -= OnDamageTaken;
            _healthController.DamageTakenWithHistory -= OnDamageTakenWithHistory;
            _healthController.Died -= OnDied;
            _healthController.Healed -= OnHealed;
            _healthController.HealedWithHistory -= OnHealedWithHistory;
            _healthController.Revived -= OnRevived;
            _healthController.ValueChanged -= OnValueChanged;
            _healthController.ValueChangedWithHistory -= OnValueChangedWithHistory;
        }

        public void Heal(
            float healValue,
            object instigator = null,
            Action<(float, float)> healedWithHistoryCallback = null,
            Action revivedCallback = null)
        {
            _healthController.Heal(healValue, instigator, healedWithHistoryCallback, revivedCallback);
        }

        public void TakeDamage(
            float damageValue,
            object instigator = null,
            Action<(float, float)> damageTakenWithHistoryCallback = null,
            Action deathCallback = null)
        {
            _healthController.TakeDamage(damageValue, instigator, damageTakenWithHistoryCallback, deathCallback);
        }

        private void OnDamageTaken(float value)
        {
            DamageTaken?.Invoke(value);
            _damageTaken?.Invoke(value);
        }

        private void OnDamageTakenWithHistory(ValueTuple<float, float> history)
        {
            DamageTakenWithHistory?.Invoke(history);
            _damageTakenWithHistory?.Invoke(new FloatPair
            {
                Item1 = history.Item1,
                Item2 = history.Item2,
            });
        }

        private void OnDied()
        {
            Died?.Invoke();
            _died?.Invoke();
        }

        private void OnHealed(float value)
        {
            Healed?.Invoke(value);
            _healed?.Invoke(value);
        }

        private void OnHealedWithHistory((float, float) history)
        {
            HealedWithHistory?.Invoke(history);
            _healedWithHistory?.Invoke(new FloatPair
            {
                Item1 = history.Item1,
                Item2 = history.Item2
            });
        }

        private void OnRevived()
        {
            Revived?.Invoke();
            _revived?.Invoke();
        }

        private void OnValueChanged(float value)
        {
            ValueChanged?.Invoke(value);
            _valueChanged?.Invoke(value);
        }

        private void OnValueChangedWithHistory((float, float) history)
        {
            ValueChangedWithHistory?.Invoke(history);
            _valueChangedWithHistory?.Invoke(new FloatPair
            {
                Item1 = history.Item1,
                Item2 = history.Item2
            });
        }
    }
}