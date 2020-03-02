using UniRx;

namespace Source.Features.PlayerStats
{
    public interface IPlayerStatsModel
    {
        IReadOnlyReactiveProperty<bool> IsAlive { get; }
        IReadOnlyReactiveProperty<int> Health { get; }
        IReadOnlyReactiveProperty<float> DistanceUnits { get; }
        IReadOnlyReactiveProperty<float> BestDistanceUnits { get; }
    }
}