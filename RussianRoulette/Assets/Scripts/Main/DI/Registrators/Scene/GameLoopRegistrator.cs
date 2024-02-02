using GameLoop;
using TatRat.GameLoop;
using TatRat.Main.DI;
using VContainer;

namespace TatRat.Main
{
    internal class GameLoopRegistrator : SceneRegistrator
    {
        public override void Register(IContainerBuilder builder)
        {
            builder.Register<MenuGameState>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<GameGameState>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<GameLoopService>(Lifetime.Scoped).AsImplementedInterfaces();
        }
    }
}