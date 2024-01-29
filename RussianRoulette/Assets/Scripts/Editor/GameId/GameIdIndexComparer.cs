using JetBrains.Annotations;
using TatRat.API;
using TatRat.Editor.GameId;

namespace TatRat.Editor
{
    [UsedImplicitly]
    public sealed class GameIdIndexComparer : IGameIdIndexComparer
    {
        private static GameIdSettings Settings => GameIdSettings.Instance;

        public int Compare(API.GameId x, API.GameId y)
        {
            var xIndex = Settings.GetIndexById(x);
            var yIndex = Settings.GetIndexById(y);

            return xIndex.CompareTo(yIndex);
        }
    }
}