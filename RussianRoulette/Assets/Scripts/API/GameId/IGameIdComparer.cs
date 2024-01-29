using System.Collections.Generic;

namespace TatRat.API
{
    public interface IGameIdComparer : IComparer<GameId>
    {
    }

    public interface IGameIdIndexComparer : IGameIdComparer
    {
    }

    public interface IGameIdNameComparer : IGameIdComparer
    {
    }
}