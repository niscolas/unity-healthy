using System;
using niscolas.UnityUtils.Core;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Events;

namespace Healthy
{
    public class HealthComponent : CachedMonoBehaviour, IHealth, IHealthEvents
    {
        private const string EventsLabel = "Events";

        [SerializeField]
        private FloatReference _current = new FloatReference(100);

        [SerializeField]
        private FloatReference _max = new FloatReference(100);

        [SerializeField]
        private FloatReference _min = new FloatReference(0);

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

        public event Action<float> Damaged;
        public event Action<FloatPair> DamagedWithHistory;
        public event Action Died;
        public event Action<float> Healed;
        public event Action<FloatPair> HealedWithHistory;
        public event Action Revived;

        public float Current
        {
            get => _current;
            set => _current.Value = value;
        }

        public bool IsInvulnerable => _isInvulnerable.Value;

        public float Max => _max;

        public float Min => _min;

        private HealthController _controller;

        protected override void Awake()
        {
            base.Awake();

            _controller = new HealthController(this);
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
            _controller.TakeRelativeDamage(value, NotifyDamaged, NotifyDeath);
        }

        public void TakeRelativeDamage(float ratio)
        {
            _controller.TakeRelativeDamage(ratio, NotifyDamaged, NotifyDeath);
        }

        private void NotifyDamaged(FloatPair history)
        {
            Damaged?.Invoke(history.Item1);
            _damageTaken?.Invoke(history.Item1);
            DamagedWithHistory?.Invoke(history);
            _damageTakenWithHistory?.Invoke(history);
        }

        private void NotifyDeath()
        {
            Died?.Invoke();
            _died?.Invoke();
        }

        private void NotifyHealed(FloatPair history)
        {
            Healed?.Invoke(history.Item1);
            _healed?.Invoke(history.Item1);
            HealedWithHistory?.Invoke(history);
            _healedWithHistory?.Invoke(history);
        }

        private void NotifyRevived()
        {
            Revived?.Invoke();
            _revived?.Invoke();
        }
    }
}