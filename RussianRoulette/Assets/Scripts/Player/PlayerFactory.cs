using JetBrains.Annotations;
using TatRat.API;
using UObject = UnityEngine.Object;

namespace TatRat.Player
{
    [UsedImplicitly]
    public class PlayerFactory : IPlayerFactory
    {
        private readonly PlayerAsset _playerAsset;

        public PlayerFactory(PlayerAsset playerAsset) => 
            _playerAsset = playerAsset;

        public IPlayer Create() => 
            UObject.Instantiate(_playerAsset.PlayerPrefab);
    }
}