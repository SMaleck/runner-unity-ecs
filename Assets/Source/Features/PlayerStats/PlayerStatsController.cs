using Source.Features.DataBridge;
using UGF.Util.UniRx;
using UniRx;

namespace Source.Features.PlayerStats
{
    public class PlayerStatsController : AbstractDisposable
    {
        private readonly PlayerStatsModel _playerStatsModel;

        public PlayerStatsController(PlayerStatsModel playerStatsModel)
        {
            _playerStatsModel = playerStatsModel;

            Observable.EveryLateUpdate()
                .Subscribe(_ => OnLateUpdate())
                .AddTo(Disposer);
        }

        private void OnLateUpdate()
        {
            if (Blackboard.TryGet(BlackboardEntryId.PlayerHealth, out int currentHealth))
            {
                _playerStatsModel.SetHealth(currentHealth);
            }

            if (Blackboard.TryGet(BlackboardEntryId.PlayerDistanceTraveled, out float distanceUnits))
            {
                _playerStatsModel.SetDistanceUnits(distanceUnits);
            }
        }
    }
}
