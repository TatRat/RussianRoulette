using TatRat.API;
using UnityEngine;

namespace TatRat.Bonuses
{
    public class BonusService : IBonusService
    {
        private readonly IBonusViewFactory _bonusViewFactory;
        private readonly IBonusesAsset _bonusesAsset;

        public BonusService(IBonusViewFactory bonusViewFactory, IBonusesAsset bonusesAsset)
        {
            _bonusViewFactory = bonusViewFactory;
            _bonusesAsset = bonusesAsset;
        }

        //TODO: добавить пул объектов 
        public AbstractBonusView GetRandomBonus()
        {
            IBonusDataConfig dataConfig = _bonusesAsset.DataPairs[Random.Range(0, _bonusesAsset.DataPairs.Count)];
            
            return _bonusViewFactory.Create(dataConfig);
        }
    }
}