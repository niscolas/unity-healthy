using System;
using UnityAtoms.BaseAtoms;

namespace Healthy
{
    public interface IHealthEvents
    {
        public event Action<float> Damaged;
        public event Action<FloatPair> DamagedWithHistory;
        public event Action Died;
        public event Action<float> Healed;
        public event Action<FloatPair> HealedWithHistory;
        public event Action Revived;
    }
}