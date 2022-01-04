using System;

namespace Healthy
{
    public interface IHealthEvents
    {
        event Action<float> DamageTaken;
        event Action<(float, float)> DamageTakenWithHistory;
        event Action Died;
        event Action<float> Healed;
        event Action<(float, float)> HealedWithHistory;
        event Action Revived;
        event Action<float> ValueChanged;
        event Action<(float, float)> ValueChangedWithHistory;
    }
}