using TatRat.Advertisements;
using TatRat.Main.DI;
using VContainer;

namespace TatRat.Main
{
    internal class AdvertisementRegistrator : AppRegistrator
    {
        public override void Register(IContainerBuilder builder) => 
            builder.Register<MockAdvertisementService>(Lifetime.Scoped).AsImplementedInterfaces();
    }
}