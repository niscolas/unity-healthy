using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Assertions;

namespace niscolas.Healthy
{
    public class HealthNotifier : MonoBehaviour
    {
        [Required, SerializeField]
        private HealthSystem _healthSystem;

        [ListDrawerSettings(NumberOfItemsPerPage = 1)]
        [SerializeField]
        private HealthComparer[] _comparers;

        private void Awake()
        {
            Assert.IsNotNull(_healthSystem);

            _healthSystem.ValueChangedWithHistory += HealthSystem_OnValueChanged;
        }

        private void OnDestroy()
        {
            _healthSystem.ValueChangedWithHistory -= HealthSystem_OnValueChanged;
        }

        private void HealthSystem_OnValueChanged(FloatPair healthWithHistory)
        {
            foreach (HealthComparer comparer in _comparers)
            {
                comparer.Execute(healthWithHistory.Item2, _healthSystem.MaxHealth);
            }
        }
    }
}