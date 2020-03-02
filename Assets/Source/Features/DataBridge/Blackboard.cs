using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Source.Installation.SceneInstallers;
using UGF.Util;

namespace Source.Features.DataBridge
{
    /// <summary>
    /// An ugly crutch to marshal data back to the mono space, a proper service locator pattern might be better,
    /// but this was a quick & easy solution for this example project
    /// </summary>
    public static class Blackboard
    {
        private static readonly Dictionary<BlackboardEntryId, object> BlackboardStore = new Dictionary<BlackboardEntryId, object>();

        static Blackboard()
        {
            ClearAndResetBlackboard();
        }

        public static void Reset([CallerFilePath] string sourceFilePath = "")
        {
            if (!sourceFilePath.Contains(nameof(GameSceneInstaller)))
            {
                UGF.Logger.Warn($"Ignored forbidden attempt to reset {nameof(Blackboard)} from {sourceFilePath}");
                return;
            }

            ClearAndResetBlackboard();
        }

        private static void ClearAndResetBlackboard()
        {
            BlackboardStore.Clear();
            EnumHelper<BlackboardEntryId>.ForEach(
                entryId => BlackboardStore.Add(entryId, null));
        }

        public static void Set(BlackboardEntryId blackboardEntryId, object value)
        {
            BlackboardStore[blackboardEntryId] = value;
        }

        [CanBeNull]
        public static object Get(BlackboardEntryId blackboardEntryId)
        {
            return BlackboardStore[blackboardEntryId];
        }

        public static bool TryGet<T>(BlackboardEntryId blackboardEntryId, out T value)
        {
            value = default(T);

            if (!BlackboardStore.ContainsKey(blackboardEntryId))
            {
                return false;
            }

            var currentValue = BlackboardStore[blackboardEntryId];
            if (currentValue == null)
            {
                return false;
            }

            value = (T) currentValue;
            return true;
        }
    }
}
