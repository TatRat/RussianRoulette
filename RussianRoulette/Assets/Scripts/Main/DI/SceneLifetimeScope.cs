using UnityEngine;

namespace TatRat.Main.DI
{
    internal sealed class SceneLifetimeScope : AbstractLifetimeScope<SceneRegistrator>
    {
#if UNITY_EDITOR
        [ContextMenu("Gather Scene Registrators")]
        private void GatherSceneRegistrators()
            => GatherRegistrators();
#endif
    }
}