using TatRat.API;

namespace TatRat.Bonuses
{
    public class BonusView : AbstractBonusView
    {
        private IBonusAction _action;
        
        public override void EnableView() => 
            gameObject.SetActive(true);

        public override void DisableView() => 
            gameObject.SetActive(false);

        public override void Initialize(IBonusAction action) => 
            _action = action;

        public override void DeInitialize() => 
            _action = default;

        public override void PerformBonus(GameId callerId) => 
            _action.Perform(callerId);
    }
}