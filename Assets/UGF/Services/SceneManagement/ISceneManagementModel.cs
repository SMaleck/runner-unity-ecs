using System;
using UniRx;

namespace UGF.Services.SceneManagement
{
    public interface ISceneManagementModel
    {
        IObservable<Unit> OnSceneStarted { get; }
    }
}
