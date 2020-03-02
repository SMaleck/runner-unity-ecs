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

        public static IObservable<bool> IfTrue(this IObservable<bool> observable)
        {
            return observable.Where(value => value);
        }

        public static IObservable<bool> IfFalse(this IObservable<bool> observable)
        {
            return observable.Where(value => !value);
        }
    }
}
