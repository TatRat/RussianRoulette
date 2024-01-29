namespace TatRat.API
{
    public interface IStateMachine
    {
        void ChangeState<TState>() where TState : class, IEnterableState;
        void ChangeState<TState, TParam>(TParam data) where TState : class, IEnterableState<TParam>;
    }
}