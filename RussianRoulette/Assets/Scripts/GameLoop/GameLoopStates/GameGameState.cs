using TatRat.API;

namespace TatRat.GameLoop
{
    public class GameGameState : GameState, IEnterableState, IExitableState
    {
        private readonly ISceneLoadService _sceneLoadService;

        public GameGameState(ISceneLoadService sceneLoadService) => 
            _sceneLoadService = sceneLoadService;

        public async void Enter() => 
            await _sceneLoadService.LoadSceneAsync(GameLoopConstants.GAME_SCENE_NAME);

        public void Exit()
        {
            
        }
    }
}