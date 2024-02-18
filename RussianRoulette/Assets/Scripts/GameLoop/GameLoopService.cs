using System.Collections.Generic;
using TatRat.API;
using TatRat.GameLoop;

namespace GameLoop
{
    public class GameLoopService : IGameLoopService
    {
        private StateMachine _gameloopStateMachine = new();

        public GameLoopService(IList<GameState> needGameStates)
        {
            foreach (GameState state in needGameStates)
            {
                state.Initialize(_gameloopStateMachine);
                _gameloopStateMachine.Add(state);
            }
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