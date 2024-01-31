using System.Threading.Tasks;
using TatRat.API;
using Unity.Plastic.Newtonsoft.Json.Serialization;

namespace TatRat.ApplicationLoop
{
    public class LoadApplicationState : IApplicationState, IEnterableState, IExitableState
    {
        public event Action ConfigApplied; 

        public async void Enter()
        {
            await ConfigureApplication();
        }

        private async Task ConfigureApplication()
        {
            // Подгружаем конфиг
            await Task.CompletedTask;
            
            // Применяем конфиг
            
            
            ConfigApplied?.Invoke();
        }

        public void Exit()
        {
            
        }
    }
}