using TatRat.Main.DI;
using TatRat.Messages;
using TatRat.SceneLoading;
using VContainer;

namespace TatRat.Main
{
    internal class ServicesRegistrator : AppRegistrator
    {
        public override void Register(IContainerBuilder builder)
        {
            builder.Register<SceneLoadService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<MessageService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}