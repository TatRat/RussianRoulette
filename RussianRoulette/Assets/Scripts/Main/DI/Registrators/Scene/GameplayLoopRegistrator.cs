using TatRat.GameplayLoop;
using TatRat.Main.DI;
using VContainer;

namespace TatRat.Main
{
    internal class GameplayLoopRegistrator : SceneRegistrator
    {
        public override void Register(IContainerBuilder builder)
        {
            builder.Register<CutsceneGameplayState>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<GetBonusesGameplayState>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<UseBonusAfterGameloopState>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<RevolverChooseGameloopState>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<UseBonusBeforeGameloopState>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<ShootingGameloopState>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<EndRoundGameloopState>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<ReloadRevolverGameloopState>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<GameOverGameplayState>(Lifetime.Scoped).AsImplementedInterfaces();

            builder.Register<GameplayLoopService>(Lifetime.Scoped).AsImplementedInterfaces();
        }
    }
}