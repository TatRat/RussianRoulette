namespace TatRat.API
{
    public interface IExitableState : IState
    {
        void Exit();
    }
}