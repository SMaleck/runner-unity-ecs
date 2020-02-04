namespace UGF.Views.Mediation
{
    public interface IClosableViewRegistrar
    {
        void RegisterClosableView(ClosableViewType closableViewType, IClosableViewController closableViewController);
    }
}
