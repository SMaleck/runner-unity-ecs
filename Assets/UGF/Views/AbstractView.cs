using UGF.Util.Mono;
using Zenject;

namespace UGF.Views
{
    public abstract class AbstractView : AbstractDisposableMonoBehaviour, IInitializable
    {
        public bool IsActive => gameObject.activeSelf;

        public void Initialize()
        {
            OnInitialize();
        }

        public virtual void OnInitialize() { }

        public void SetActive(bool isActive)
        {
            this.gameObject.SetActive(isActive);
        }
    }
}
