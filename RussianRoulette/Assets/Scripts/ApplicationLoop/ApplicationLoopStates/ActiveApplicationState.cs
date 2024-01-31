using System;
using TatRat.API;
using UnityEngine;

namespace TatRat.ApplicationLoop
{
    public class ActiveApplicationState : IApplicationState, IEnterableState, IExitableState
    {
        public event Action ApplicationDisabled;
        
        private readonly IGameLoopService _gameLoopService;

        public ActiveApplicationState(IGameLoopService gameLoopService)
        {
            _gameLoopService = gameLoopService;
        }

        public void Enter()
        {
            if (_gameLoopService.IsStarted)
                _gameLoopService.LoadMenu();
            
            _gameLoopService.UnFreezeGame();

            Application.focusChanged += OnFocusChanged;
        }

        public void Exit()
        {
            _gameLoopService.FreezeGame();
            
            Application.focusChanged -= OnFocusChanged;
        }

        private void OnFocusChanged(bool isFocused)
        {
            if (isFocused)
                return;
            
            ApplicationDisabled?.Invoke();
        }
    }
}