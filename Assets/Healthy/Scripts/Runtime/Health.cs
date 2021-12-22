using System;
using niscolas.UnityUtils.Core;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Events;

namespace niscolas.Healthy
{
    public class Health : CachedMonoBehaviour, IDamageable, IHealable, IHealth
    {
        private const string EventsLabel = "Events";

        [SerializeField]
        private FloatReference _health;

        [SerializeField]
        private BoolReference _isInvulnerable = new BoolReference(true);

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
        public event Action<FloatPair> DamageTakenWithHistory;
        public event Action Died;
        public event Action<float> Healed;
        public event Action<FloatPair> HealedWithHistory;
        public event Action Revived;
        public event Action<float> ValueChanged;
        public event Action<FloatPair> ValueChangedWithHistory;

        public float Current
        {
            get => _health.Value;
            set
            {
                _previousHealth = _health.Value;
                _health.Value = value;
            }
        }

        public bool IsInvulnerable => _isInvulnerable.Value;

        public float Max { get; private set; }

        private HealthController _controller;

        private float _previousHealth;

        protected override void Awake()
        {
            base.Awake();

            _controller = new HealthController(
                this,
                newHealthValue => Current = newHealthValue);

            Max = Current;
            _previousHealth = Max;
        }

        public void HealRaw(float value)
        {
            _controller.HealRaw(value, NotifyHealed, NotifyRevived);
        }

        public void HealRelative(float ratio)
        {
            _controller.HealRelative(ratio, NotifyHealed, NotifyRevived);
        }

        public void TakeRawDamage(float value)
        {
            _controller.TakeRelativeDamage(value, NotifyDamageTaken, NotifyDeath);
        }

        public void TakeRelativeDamage(float ratio)
        {
            _controller.TakeRelativeDamage(ratio, NotifyDamageTaken, NotifyDeath);
        }

        private FloatPair GetHealthHistory()
        {
            return new FloatPair {Item1 = _previousHealth, Item2 = Current};
        }

        private void NotifyDamageTaken(float current)
        {
            FloatPair healthHistory = GetHealthHistory();
            NotifyValueChanged(current, healthHistory);

            DamageTaken?.Invoke(current);
            DamageTakenWithHistory?.Invoke(healthHistory);
            _damageTaken?.Invoke(current);
            _damageTakenWithHistory?.Invoke(healthHistory);
        }

        private void NotifyDeath()
        {
            Died?.Invoke();
            _died?.Invoke();
        }

        private void NotifyHealed(float current)
        {
            FloatPair healthHistory = GetHealthHistory();
            NotifyValueChanged(current, healthHistory);

            Healed?.Invoke(current);
            HealedWithHistory?.Invoke(healthHistory);
            _healed?.Invoke(current);
            _healedWithHistory?.Invoke(healthHistory);
        }

        private void NotifyRevived()
        {
            Revived?.Invoke();
            _revived?.Invoke();
        }

        private void NotifyValueChanged(float current, FloatPair healthHistory)
        {
            ValueChanged?.Invoke(current);
            ValueChangedWithHistory?.Invoke(healthHistory);
            _valueChanged?.Invoke(current);
            _valueChangedWithHistory?.Invoke(healthHistory);
        }
    }
}