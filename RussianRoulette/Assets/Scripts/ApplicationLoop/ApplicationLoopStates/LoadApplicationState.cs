using System;
using System.Threading.Tasks;
using TatRat.API;

namespace TatRat.ApplicationLoop
{
    public class LoadApplicationState : IApplicationState, IEnterableState, IExitableState
    {
        public event Action ConfigApplied;
        
        private readonly IPlatformDataLoader _platformDataLoader;

        public LoadApplicationState(IPlatformDataLoader platformDataLoader) => 
            _platformDataLoader = platformDataLoader;

        public async void Enter()
        {
            await ConfigureApplication();
        }

        private async Task ConfigureApplication()
        {
            // Подгружаем конфиг
            _platformDataLoader.TryToLoad(out string stringJSON);
            
            // Применяем конфиг
            
            
            ConfigApplied?.Invoke();
        }

        public void Exit()
        {
            
        }
    }
}
