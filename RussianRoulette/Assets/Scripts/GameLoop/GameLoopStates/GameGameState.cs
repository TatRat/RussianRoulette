using TatRat.API;

namespace TatRat.GameLoop
{
    public class GameGameState : IEnterableState, IExitableState
    {
        private const string GameSceneName = "GameScene";
        
        private readonly ISceneLoadService _sceneLoadService;

        public GameGameState(ISceneLoadService sceneLoadService)
        {
            _sceneLoadService = sceneLoadService;
        }
        
        public async void Enter()
        {
            await _sceneLoadService.LoadSceneAsync(GameSceneName);
        }

        public void Exit()
        {
            
        }
    }
}