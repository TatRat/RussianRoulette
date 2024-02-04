using System.Collections.Generic;

namespace TatRat.API
{
    public interface IBonusesAsset
    {
        public IReadOnlyList<IBonusDataConfig> DataPairs { get; }
    }
}