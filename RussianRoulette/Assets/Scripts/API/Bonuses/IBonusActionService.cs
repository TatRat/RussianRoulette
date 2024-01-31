namespace TatRat.API
{
    public interface IBonusActionService : IService
    {
        public IBonusAction GetBonusActionByData(IBonusActionData data);
    }
}