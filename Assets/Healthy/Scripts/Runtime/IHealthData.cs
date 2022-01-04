namespace Healthy
{
    public interface IHealthData
    {
        float Current { get; set; }
        bool CanTakeDamage { get; set; }
        bool CanHeal { get; set; }
        float Max { get; set; }
        float Min { get; set; }
    }
}