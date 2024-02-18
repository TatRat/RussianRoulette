using TatRat.API;

namespace TatRat.GameLoop
{
    public class MenuGameState : GameState, IEnterableState, IExitableState
    {
        private readonly ISceneLoadService _sceneLoadService;

        public MenuGameState(ISceneLoadService sceneLoadService) => 
            _sceneLoadService = sceneLoadService;

        public async void Enter() => 
            await _sceneLoadService.LoadSceneAsync(GameLoopConstants.MENU_SCENE_NAME);

        public void Exit()
        {
            
        }
    }
}