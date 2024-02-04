namespace TatRat.API
{
    public interface IBonusDataConfig
    {
        public GameId BonusId { get; }
        public IBonusActionData BonusActionData { get; }
        public AbstractBonusView BonusView { get; }
    }
}