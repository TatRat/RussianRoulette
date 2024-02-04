using TatRat.API;
using TatRat.Bonuses;
using TatRat.Main.DI;
using UnityEngine;
using VContainer;

namespace TatRat.Main
{
    internal class BonusesAssetsRegistrator : AppRegistrator
    {
        [SerializeField] [CheckObject] private BonusesDataAsset _bonusesDataAsset; 
            
        public override void Register(IContainerBuilder builder)
        {
            builder.RegisterInstance(_bonusesDataAsset).AsImplementedInterfaces();
        }
    }
}