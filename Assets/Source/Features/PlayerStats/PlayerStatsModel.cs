using System.Linq;
using UGF.Util.UniRx;
using UniRx;

namespace Source.Features.PlayerStats
{
    public class PlayerStatsModel : AbstractDisposable, IPlayerStatsModel
    {
        public IReadOnlyReactiveProperty<bool> IsAlive;

        private readonly ReactiveProperty<int> _health;
        public IReadOnlyReactiveProperty<int> Health => _health;

        private readonly ReactiveProperty<float> _distanceUnits;
        public IReadOnlyReactiveProperty<float> DistanceUnits => _distanceUnits;

        public PlayerStatsModel()
        {
            _health = new ReactiveProperty<int>().AddTo(Disposer);
            _distanceUnits = new ReactiveProperty<float>().AddTo(Disposer);

            IsAlive = _health.Select(health => health > 0)
                .ToReadOnlyReactiveProperty()
                .AddTo(Disposer);
        }

        public void SetHealth(int value)
        {
            _health.Value = value;
        }

        public void SetDistanceUnits(float value)
        {
            _distanceUnits.Value = value;
        }
    }
}
