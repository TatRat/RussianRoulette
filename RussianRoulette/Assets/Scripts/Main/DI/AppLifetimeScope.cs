using UnityEngine;
using VContainer;

namespace TatRat.Main.DI
{
    internal sealed class AppLifetimeScope : AbstractLifetimeScope<AppRegistrator>
    {
        protected override void Configure(IContainerBuilder builder)
        {
            DontDestroyOnLoad(gameObject);
            base.Configure(builder);
        }
        
#if UNITY_EDITOR
        [ContextMenu("Gather App Registrators")]
        private void GatherAppRegistrators()
            => GatherRegistrators();
#endif
    }
}