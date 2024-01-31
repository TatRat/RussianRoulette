using System.Collections.Generic;
using TatRat.API;

namespace TatRat.GameplayLoop
{
    public class GameplayLoopService : IGameplayLoopService
    {
        private readonly StateMachine _stateMachine = new();
        
        public GameplayLoopService(IList<IGameplayLoopState> needStates)
        {
            foreach (IGameplayLoopState state in needStates) 
                _stateMachine.Add(state);
        }
    }
}