using System;
using niscolas.UnityAtomsUtils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace niscolas.Healthy
{
    [Serializable]
    public struct HealthComparer
    {
        [HideLabel, HideReferenceObjectPicker, InlineProperty, SerializeField]
        private FloatComparisons _comparisons;

        [Header("Events")]
        [SerializeField]
        private UnityEvent _successEvent;

        [SerializeField]
        private UnityEvent _failEvent;

        [Header("Debug")]
        [ReadOnly, SerializeField]
        private bool _executedLastTime;

        public void TryExecute(float newValue, float maxValue)
        {
            float currentRatio = newValue / maxValue;
            if (_comparisons.CompareAll(currentRatio))
            {
                if (_executedLastTime)
                {
                    return;
                }

                _executedLastTime = true;
                _successEvent?.Invoke();
            }
            else
            {
                _executedLastTime = false;
                _failEvent?.Invoke();
            }
        }
    }
}