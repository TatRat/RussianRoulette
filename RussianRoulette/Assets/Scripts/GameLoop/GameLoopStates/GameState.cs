using TatRat.API;

namespace TatRat.GameLoop
{
    public abstract class GameState : IState
    {
        protected StateMachine StateMachine;

        public void Initialize(StateMachine stateMachine) => 
            StateMachine = stateMachine;
    }
}