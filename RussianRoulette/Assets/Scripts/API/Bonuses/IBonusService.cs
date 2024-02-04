namespace TatRat.API
{
    public interface IBonusService : IService
    {
        public AbstractBonusView GetRandomBonus();
    }
}