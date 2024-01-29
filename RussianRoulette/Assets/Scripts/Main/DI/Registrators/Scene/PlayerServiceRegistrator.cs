using TatRat.Main.DI;
using TatRat.Player;
using VContainer;

namespace TatRat.Main
{
    internal class PlayerServiceRegistrator : SceneRegistrator
    {
        public override void Register(IContainerBuilder builder)
        {
            builder.Register<PlayerFactory>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<PlayerService>(Lifetime.Scoped).AsImplementedInterfaces();
        }
    }
}