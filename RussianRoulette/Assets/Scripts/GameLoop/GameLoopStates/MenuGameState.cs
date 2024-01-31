using TatRat.API;

namespace TatRat.GameLoop
{
    public class MenuGameState : IEnterableState, IExitableState
    {
        private const string MenuSceneName = "Menu";
        
        private readonly ISceneLoadService _sceneLoadService;

        public MenuGameState(ISceneLoadService sceneLoadService)
        {
            _sceneLoadService = sceneLoadService;
        }

        public async void Enter()
        {
            await _sceneLoadService.LoadSceneAsync(MenuSceneName);
        }

        public void Exit()
        {
            
        }
    }
}