using Source.Features.PlayerStats;
using UGF.Util.UniRx;
using UGF.Views.Mediation;
using UniRx;

namespace Source.Features.RunEnd
{
    public class RunEndController : AbstractDisposable
    {
        public RunEndController(
            IPlayerStatsModel playerStatsModel,
            IClosableViewMediator closableViewMediator)
        {
            playerStatsModel.IsAlive
                .IfFalse()
                .Subscribe(_ => closableViewMediator.Open(ClosableViewType.RunEnd))
                .AddTo(Disposer);
        }
    }
}
