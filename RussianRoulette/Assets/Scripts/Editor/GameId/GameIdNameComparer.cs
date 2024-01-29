using System;
using JetBrains.Annotations;
using TatRat.API;
using TatRat.Editor.GameId;

namespace TatRat.Editor
{
    [UsedImplicitly]
    public sealed class GameIdNameComparer : IGameIdNameComparer
    {
        private static GameIdSettings Settings => GameIdSettings.Instance;

        public int Compare(API.GameId x, API.GameId y)
        {
            var xName = Settings.GetIdName(x);
            var yName = Settings.GetIdName(y);

            return string.Compare(xName, yName, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}