using System.Threading.Tasks;
using TatRat.API;

namespace TatRat.ApplicationLoop
{
    public class LoadApplicationState : ApplicationState, IEnterableState, IExitableState
    {
        private readonly IPlatformDataLoader _platformDataLoader;

        public LoadApplicationState(IPlatformDataLoader platformDataLoader) => 
            _platformDataLoader = platformDataLoader;

        public async void Enter() => 
            await ConfigureApplication();

        private async Task ConfigureApplication()
        {
            // Подгружаем конфиг
            _platformDataLoader.TryToLoad(out string stringJSON);
            
            // Применяем конфиг
            
            StateMachine.ChangeState<ActiveApplicationState>();
        }

        public void Exit()
        {
            
        }
    }
}
