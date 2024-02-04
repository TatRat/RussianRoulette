using TatRat.Bonuses;
using TatRat.Main.DI;
using VContainer;

namespace TatRat.Main
{
    internal class BonusesServiceRegistrator : SceneRegistrator
    {
        public override void Register(IContainerBuilder builder)
        {
            builder.Register<BonusActionService>(Lifetime.Scoped).AsImplementedInterfaces();
            
            builder.Register<BonusViewFactory>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<BonusService>(Lifetime.Scoped).AsImplementedInterfaces();
        }
    }
}