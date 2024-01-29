using TatRat.Main.DI;
using VContainer;

namespace TatRat.Main
{
    internal class ApplicationLoopRegistrator : AppRegistrator
    {
        public override void Register(IContainerBuilder builder)
        {
            //регистрация app-лупа
            builder.RegisterBuildCallback(OnContainerBuild);
        }

        private void OnContainerBuild(IObjectResolver container)
        {
            //Запуск app-лупа
        }
    }
}