namespace TatRat.API
{
    public interface IBonusAction
    {
        public void Perform(GameId callerId);
    }
}