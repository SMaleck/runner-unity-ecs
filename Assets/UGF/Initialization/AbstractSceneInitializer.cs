using System.Linq;
using UGF.Views;
using UGF.Views.Mediation;
using Zenject;

namespace UGF.Initialization
{
    public abstract class AbstractSceneInitializer : ISceneInitializer
    {
        [Inject] private readonly IClosableViewRegistrar _closableViewRegistrar;
        [Inject] private readonly ClosableViewController.Factory _closableViewControllerFactory;

        public abstract void Initialize();

        protected void SetupView<T>(T view) where T : AbstractView
        {
            var listOfInits = view.GetComponents<IInitializable>()
                .Where(viewItem => viewItem.GetType() != typeof(T))
                .ToList();

            listOfInits.ForEach(component => component.Initialize());

            view.GetComponentsInChildren<IInitializable>()
                .ToList()
                .ForEach(component => component.Initialize());
        }

        protected void SetupClosableView(AbstractView abstractView)
        {
            SetupView(abstractView);

            var closableView = abstractView.GetComponent<IClosableView>();
            var closableViewController = _closableViewControllerFactory.Create(closableView);
        }

        protected void SetupClosableView<T>(T abstractView, ClosableViewType closableViewType) where T : AbstractView
        {
            SetupView(abstractView);

            var closableView = abstractView.GetComponent<IClosableView>();
            var closableViewController = _closableViewControllerFactory.Create(closableView);

            _closableViewRegistrar.RegisterClosableView(closableViewType, closableViewController);
        }
    }
}
