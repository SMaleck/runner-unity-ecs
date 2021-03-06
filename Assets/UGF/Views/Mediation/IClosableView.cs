﻿using System;
using UniRx;

namespace UGF.Views.Mediation
{
    public interface IClosableView
    {
        void Open();
        void Close();

        bool IsOpen { get; }

        IObservable<Unit> OnViewOpen { get; }
        IObservable<Unit> OnViewOpenCompleted { get; }

        IObservable<Unit> OnViewClose { get; }
        IObservable<Unit> OnViewCloseCompleted { get; }

        IObservable<Unit> OnCloseClicked { get; }
    }
}
