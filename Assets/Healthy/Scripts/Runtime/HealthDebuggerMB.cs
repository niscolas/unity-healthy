using niscolas.UnityUtils.Core;
using UnityEngine;

namespace Healthy
{
    [AddComponentMenu(Constants.AddComponentMenuPrefix + "Health Debugger")]
    public class HealthDebuggerMB : CachedMB
    {
        [SerializeField]
        private HealthMB _health;

        [SerializeField]
        private float _healValue = 1;

        [SerializeField]
        private float _damageValue = 1;

        [ContextMenu(nameof(Heal))]
        private void Heal()
        {
            _health.Heal(_healValue);
        }

        [ContextMenu(nameof(TakeDamage))]
        private void TakeDamage()
        {
            _health.TakeDamage(_damageValue);
        }
    }
}