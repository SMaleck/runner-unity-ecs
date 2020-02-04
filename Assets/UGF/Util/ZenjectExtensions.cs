using Zenject;

namespace UGF.Util
{
    public static class ZenjectExtensions
    {
        public static IfNotBoundBinder AsSingleNonLazy(this ScopeConcreteIdArgConditionCopyNonLazyBinder binder)
        {
            return binder.AsSingle().NonLazy();
        }

        public static ConditionCopyNonLazyBinder BindPrefabFactory<TContract, TFactory>(
            this DiContainer diContainer)
            where TFactory : PlaceholderFactory<UnityEngine.Object, TContract>
        {
            return diContainer.BindFactory<UnityEngine.Object, TContract, TFactory>()
                .FromFactory<PrefabFactory<TContract>>();
        }

        public static ConditionCopyNonLazyBinder BindPrefabFactory<TContract, TContract2, TFactory>(
            this DiContainer diContainer)
            where TFactory : PlaceholderFactory<UnityEngine.Object, TContract2, TContract>
        {
            return diContainer.BindFactory<UnityEngine.Object, TContract2, TContract, TFactory>()
                .FromFactory<PrefabFactory<TContract2, TContract>>();
        }

        public static ConditionCopyNonLazyBinder BindPrefabFactory<TContract, TContract2, TContract3, TFactory>(
            this DiContainer diContainer)
            where TFactory : PlaceholderFactory<UnityEngine.Object, TContract3, TContract2, TContract>
        {
            return diContainer.BindFactory<UnityEngine.Object, TContract3, TContract2, TContract, TFactory>()
                .FromFactory<PrefabFactory<TContract3, TContract2, TContract>>();
        }

        public static ConditionCopyNonLazyBinder BindPrefabFactory<TContract, TContract2, TContract3, TContract4, TFactory>(
            this DiContainer diContainer)
            where TFactory : PlaceholderFactory<UnityEngine.Object, TContract4, TContract3, TContract2, TContract>
        {
            return diContainer.BindFactory<UnityEngine.Object, TContract4, TContract3, TContract2, TContract, TFactory>()
                .FromFactory<PrefabFactory<TContract4, TContract3, TContract2, TContract>>();
        }

        public static ConditionCopyNonLazyBinder BindPrefabFactory<TContract, TContract2, TContract3, TContract4, TContract5, TFactory>(
            this DiContainer diContainer)
            where TFactory : PlaceholderFactory<UnityEngine.Object, TContract5, TContract4, TContract3, TContract2, TContract>
        {
            return diContainer.BindFactory<UnityEngine.Object, TContract5, TContract4, TContract3, TContract2, TContract, TFactory>()
                .FromFactory<PrefabFactory<TContract5, TContract4, TContract3, TContract2, TContract>>();
        }
    }
}
