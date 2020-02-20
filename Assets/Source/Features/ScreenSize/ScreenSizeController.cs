using Source.Features.DataBridge;
using UGF.Util.UniRx;

namespace Source.Features.ScreenSize
{
    public class ScreenSizeController : AbstractDisposable
    {
        public ScreenSizeController(ScreenSizeModel screenSizeModel)
        {
            Blackboard.Set(
                BlackboardEntryId.CameraWidthExtendUnits,
                screenSizeModel.WidthExtendUnits);
        }
    }
}
