using Platform.MockPlatform;
using TatRat.Main.DI;
using VContainer;

namespace TatRat.Main
{
    internal class PlatformRegistrator : AppRegistrator
    {
        public override void Register(IContainerBuilder builder)
        {
            builder.Register<MockPlatformDataSaver>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<MockPlatformDataLoader>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<MockPlatformService>(Lifetime.Scoped).AsImplementedInterfaces();
        }
    }
}