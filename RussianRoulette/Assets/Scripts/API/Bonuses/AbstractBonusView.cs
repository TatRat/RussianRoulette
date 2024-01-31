using UnityEngine;

namespace TatRat.API
{
    public abstract class AbstractBonusView : MonoBehaviour
    {
        [SerializeReference] [TypeSelect] private IBonusActionData _data;

        public IBonusActionData Data => _data;

        public abstract void EnableView();
        public abstract void DisableView();
        public abstract void Initialize(IBonusAction action);
        public abstract void PerformBonus(GameId callerId);
    }
}