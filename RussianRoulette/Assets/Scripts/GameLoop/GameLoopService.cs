using System.Collections.Generic;
using TatRat.API;
using TatRat.API.API.GameloopService;
using TatRat.GameLoop;

namespace GameLoop
{
    public class GameLoopService : IGameLoopService
    {
        private StateMachine _gameloopStateMachine = new();

        public GameLoopService(IList<IGameState> needGameStates)
        {
            foreach (IGameState state in needGameStates) 
                _gameloopStateMachine.Add(state);
        }

        public bool IsStarted { get; private set; } = false;

        public void LoadMenu()
        {
            _gameloopStateMachine.ChangeState<MenuGameState>();

            IsStarted = true;
        }

        public void FreezeGame()
        {
            // Ставим паузу, отключаем звук
        }

        public void UnFreezeGame()
        {
            // Возобновляем игру, включаем звук
        }
    }
}