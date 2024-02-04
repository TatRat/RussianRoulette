using System;
using System.Collections.Generic;
using TatRat.API;
using UnityEngine;

namespace TatRat.Bonuses
{
    [CreateAssetMenu(menuName = BonusesConstants.BONUSES_ASSETS_ROOT + "Bonuses Asset")]
    public class BonusesDataAsset : ScriptableObject, IBonusesAsset
    {
        [SerializeField] private BonusDataConfig[] _dataPairs = Array.Empty<BonusDataConfig>();

        public IReadOnlyList<IBonusDataConfig> DataPairs => _dataPairs;
    }
}