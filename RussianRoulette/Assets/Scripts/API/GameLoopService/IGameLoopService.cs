namespace TatRat.API.API.GameloopService
{
    public interface IGameLoopService
    {
        bool IsStarted { get; }
        
        public void LoadMenu();
        public void FreezeGame();
        public void UnFreezeGame();
    }
}