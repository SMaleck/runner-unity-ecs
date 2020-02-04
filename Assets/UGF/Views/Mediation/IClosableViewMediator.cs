using System;

namespace UGF.Views.Mediation
{
    public interface IClosableViewMediator
    {
        void Open(ClosableViewType closableViewType);
        void Close(ClosableViewType closableViewType);

        bool IsViewOpen(ClosableViewType closableViewType);

        IObservable<ClosableViewType> OnViewOpened { get; }
        IObservable<ClosableViewType> OnViewClosed { get; }
    }
}
