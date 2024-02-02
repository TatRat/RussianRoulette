using TatRat.ApplicationLoop;
using TatRat.Main.DI;
using VContainer;

namespace TatRat.Main
{
    internal class ApplicationLoopRegistrator : AppRegistrator
    {
        public override void Register(IContainerBuilder builder)
        {
            builder.Register<LoadApplicationState>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<ActiveApplicationState>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<InactiveApplicationState>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<ApplicationLoopService>(Lifetime.Scoped).AsImplementedInterfaces();
                
            builder.RegisterBuildCallback(OnContainerBuild);
        }

        private void OnContainerBuild(IObjectResolver container)
        {
            container.Resolve<ApplicationLoopService>().StartApplication();
        }
    }
}