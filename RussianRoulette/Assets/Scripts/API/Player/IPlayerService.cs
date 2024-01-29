namespace TatRat.API
{
    public interface IPlayerService : IService
    {
        public IPlayer CreatePlayer();
        public IPlayer GetPlayer();
        public void DismissPlayer();
    }
}