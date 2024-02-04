using System;
using TatRat.API;
using UnityEngine;

namespace TatRat.Bonuses
{
    [Serializable]
    public class BonusDataConfig : IBonusDataConfig
    {
        [SerializeField] private GameId _bonusId;
        [SerializeField] private AbstractBonusView _bonusView;
        [SerializeReference] [TypeSelect] private IBonusActionData _bonusActionData;

        public GameId BonusId => _bonusId;
        public IBonusActionData BonusActionData => _bonusActionData;
        public AbstractBonusView BonusView => _bonusView;
    }
}