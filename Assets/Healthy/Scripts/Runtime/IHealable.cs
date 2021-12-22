using System;
using UnityAtoms.BaseAtoms;

namespace niscolas.Healthy
{
    public interface IHealable
    {
        public event Action<float> Healed;
        public event Action<FloatPair> HealedWithHistory;

        void HealRaw(float value);
        void HealRelative(float ratio);
    }
}