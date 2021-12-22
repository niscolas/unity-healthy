using System;
using UnityAtoms.BaseAtoms;

namespace niscolas.Healthy
{
    public interface IDamageable
    {
        public event Action<float> DamageTaken;
        public event Action<FloatPair> DamageTakenWithHistory;
        public event Action Died;

        void TakeRawDamage(float value);
        void TakeRelativeDamage(float ratio);
    }
}