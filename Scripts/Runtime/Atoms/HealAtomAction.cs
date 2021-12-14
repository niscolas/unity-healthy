using niscolas.UnityUtils.UnityAtoms;
using UnityAtoms;
using UnityEngine;

namespace niscolas.Healthy.Atoms
{
    [CreateAssetMenu(
        menuName = UnityAtomsConstants.ActionsCreateAssetMenuPrefix + "(" + nameof(HealthSystem) + ") => Heal")]
    public class HealAtomAction : AtomAction<HealthSystem>
    {
        [SerializeField]
        private RawAndRatioValue _healAmount;

        public override void Do(HealthSystem healable)
        {
            if (!healable)
            {
                return;
            }

            healable.Heal(_healAmount.RawValue);
            healable.HealRelative(_healAmount.Ratio);
        }
    }
}