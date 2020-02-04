using System;
using System.Linq;
using UniRx;

namespace UGF.Util.UniRx
{
    public static class UniRxExtensions
    {
        public static IObservable<T> OncePerFrame<T>(this IObservable<T> observable)
        {
            return observable.BatchFrame().Select(batch => batch.Last());
        }
    }
}
