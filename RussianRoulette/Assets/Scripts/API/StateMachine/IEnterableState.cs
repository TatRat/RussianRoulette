namespace TatRat.API
{
    public interface IEnterableState : IState
    {
        void Enter();
    }

    public interface IEnterableState<in TParam> : IState
    {
        void Enter(TParam data);
    }
}