using TatRat.API;
using TatRat.Main.DI;
using TatRat.Player;
using UnityEngine;
using VContainer;

namespace TatRat.Main
{
    internal class PlayerAssetRegistrator : AppRegistrator
    {
        [SerializeField] [CheckObject] private PlayerAsset _playerAsset;
        
        public override void Register(IContainerBuilder builder) => 
            builder.RegisterInstance(_playerAsset).As<PlayerAsset>();
    }
}