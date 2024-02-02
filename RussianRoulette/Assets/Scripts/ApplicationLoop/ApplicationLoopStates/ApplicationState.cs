using TatRat.API;

namespace TatRat.ApplicationLoop
{
    public abstract class ApplicationState : IState
    {
        protected StateMachine StateMachine;

        public void Initialize(StateMachine stateMachine) => 
            StateMachine = stateMachine;
    }
}