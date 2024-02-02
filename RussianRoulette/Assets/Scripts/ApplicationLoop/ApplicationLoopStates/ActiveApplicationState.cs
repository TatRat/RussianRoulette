using TatRat.API;

namespace TatRat.ApplicationLoop
{
    public class ActiveApplicationState : ApplicationState, IEnterableState, IExitableState
    {
        private readonly IGameLoopService _gameLoopService;
        private readonly IPlatformService _platformService;

        public ActiveApplicationState(IGameLoopService gameLoopService, IPlatformService platformService)
        {
            _gameLoopService = gameLoopService;
            _platformService = platformService;
        }

        public void Enter()
        {
            if (_gameLoopService.IsStarted)
                _gameLoopService.LoadMenu();
            
            _gameLoopService.UnFreezeGame();
            
            _platformService.ApplicationVisibilityTypeChanged += OnFocusChanged;
        }

        public void Exit()
        {
            _gameLoopService.FreezeGame();
            
            _platformService.ApplicationVisibilityTypeChanged -= OnFocusChanged;
        }

        private void OnFocusChanged(ApplicationVisibilityType applicationVisibilityType)
        {
            if (applicationVisibilityType == ApplicationVisibilityType.Active)
                return;
            
            StateMachine.ChangeState<InactiveApplicationState>();
        }
    }
}
