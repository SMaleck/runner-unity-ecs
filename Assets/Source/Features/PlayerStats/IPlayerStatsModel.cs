using UniRx;

namespace Source.Features.PlayerStats
{
    public interface IPlayerStatsModel
    {
        IReadOnlyReactiveProperty<int> Health { get; }
        IReadOnlyReactiveProperty<float> DistanceUnits { get; }
    }
}