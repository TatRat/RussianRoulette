using TatRat.API;
using UObject = UnityEngine.Object;

namespace TatRat.Player
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerFactory _playerFactory;
        private IPlayer _player;

        public PlayerService(IPlayerFactory playerFactory) => 
            _playerFactory = playerFactory;

        public IPlayer CreatePlayer()
        {
            IPlayer player = _playerFactory.Create();
            player.Enable();
            return player;
        }

        public IPlayer GetPlayer() => 
            _player;

        public void DismissPlayer()
        {
            _player.Disable();
            UObject.Destroy(_player.GameObject);
        }
    }
}