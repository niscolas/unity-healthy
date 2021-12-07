using niscolas.UnityUtils.Core;
using niscolas.UnityUtils.UnityAtoms;
using UnityAtoms;
using UnityEngine;
using UnityUtils;

namespace niscolas.Healthy.Atoms
{
    [CreateAssetMenu(
        menuName = UnityAtomsConstants.ActionsCreateAssetMenuPrefix + "(" + nameof(HealthSystem) + ") => Deal Damage")]
    public class DealDamageAtomAction : AtomAction<GameObject>
    {
        [SerializeField]
        private RawAndRatioValue _damage;

        [SerializeField]
        private bool _findFromRoot;

        public override void Do(GameObject target)
        {
            IDamageable damageable = default;
            if (_findFromRoot && !target.TryGetComponentFromRoot(out damageable) ||
                !_findFromRoot && !target.TryGetComponent(out damageable) ||
                damageable == null)
            {
                return;
            }

            damageable.TakeRelativeDamage(_damage.Ratio);
            damageable.TakeDamage(_damage.RawValue);
        }
    }
}