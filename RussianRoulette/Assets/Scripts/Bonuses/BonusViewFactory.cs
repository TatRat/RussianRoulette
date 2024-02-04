using JetBrains.Annotations;
using TatRat.API;
using UObject = UnityEngine.Object;

namespace TatRat.Bonuses
{
    [UsedImplicitly]
    public class BonusViewFactory : IBonusViewFactory
    {
        private readonly IBonusActionService _bonusActionService;

        public BonusViewFactory(IBonusActionService bonusActionService) => 
            _bonusActionService = bonusActionService;

        public AbstractBonusView Create(IBonusDataConfig input)
        {
            IBonusAction action = _bonusActionService.GetBonusActionByData(input.BonusActionData);
            AbstractBonusView view = UObject.Instantiate(input.BonusView);
            
            view.Initialize(action);
            return view;
        }
    }
}