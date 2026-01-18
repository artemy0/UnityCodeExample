using R3;

namespace UI.ProjectSpecific.Model
{
    public interface IPlayerHealth
    {
        ReadOnlyReactiveProperty<bool> IsHealthInfinity { get; }
        ReadOnlyReactiveProperty<int> CurrentHealth { get; }
        ReadOnlyReactiveProperty<int> MaxHealth { get; }

        void DecreaseHealth(int amount = 1);
        void RestoreHealth();
    }
}