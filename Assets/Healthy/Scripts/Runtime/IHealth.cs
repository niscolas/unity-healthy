using System;
using UnityAtoms.BaseAtoms;

namespace niscolas.Healthy
{
    public interface IHealth
    {
        public event Action<float> ValueChanged;
        public event Action<FloatPair> ValueChangedWithHistory;
        
        float Max { get; }
        float Current { get; }
        bool IsInvulnerable { get; }
    }
}