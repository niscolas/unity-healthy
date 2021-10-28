using System;
using niscolas.UnityUtils.Core;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Events;

namespace niscolas.Healthy
{
    public class HealthSystem : CachedMonoBehaviour, IHealable, IDamageable
    {
        private const string EventsLabel = "Events";

        [SerializeField]
        private FloatReference _health;

        [SerializeField]
        private BoolReference _isDead;

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

        public event Action<FloatPair> ValueChangedWithHistory;

        private FloatPair HealthHistory => new FloatPair {Item1 = _previousHealth, Item2 = HealthValue};

        private float HealthValue
        {
            get => _health.Value;
            set
            {
                _previousHealth = _health.Value;
                _health.Value = value;
            }
        }

        public float MaxHealth { get; private set; }

        private float _previousHealth;

        protected override void Awake()
        {
            base.Awake();

            MaxHealth = HealthValue;
            _previousHealth = MaxHealth;
        }

        public void TakeRelativeDamage(float ratio)
        {
            float rawDamage = MaxHealth * ratio;
            TakeDamage(rawDamage);
        }

        public void TakeDamage(float rawValue)
        {
            HealthValue -= rawValue;

            NotifyDamageTaken();

            if (HealthValue > 0 || _isDead.Value)
            {
                return;
            }

            _died?.Invoke();
            _isDead.Value = true;
        }

        public void HealRelative(float ratio)
        {
            float rawHeal = MaxHealth * ratio;
            Heal(rawHeal);
        }

        public void Heal(float rawValue)
        {
            bool isBeingRevived = CheckIsBeingRevived();

            if (HealthValue + rawValue >= MaxHealth)
            {
                HealthValue = MaxHealth;
            }
            else
            {
                HealthValue += rawValue;
            }

            NotifyHealed();

            if (isBeingRevived)
            {
                NotifyRevived();
            }
        }

        private bool CheckIsBeingRevived()
        {
            return HealthValue <= 0;
        }

        private void NotifyDamageTaken()
        {
            FloatPair healthHistory = HealthHistory;
            NotifyValueChanged();
            _damageTaken?.Invoke(HealthValue);
            _damageTakenWithHistory?.Invoke(healthHistory);
        }

        private void NotifyValueChanged()
        {
            _valueChanged?.Invoke(HealthValue);
            _valueChangedWithHistory?.Invoke(HealthHistory);
            ValueChangedWithHistory?.Invoke(HealthHistory);
        }

        private void NotifyHealed()
        {
            FloatPair healthHistory = HealthHistory;
            NotifyValueChanged();
            _healed?.Invoke(HealthValue);
            _healedWithHistory?.Invoke(healthHistory);
        }

        private void NotifyRevived()
        {
            _revived?.Invoke();
        }
    }
}