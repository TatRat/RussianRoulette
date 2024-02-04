using UnityEngine;

namespace TatRat.API
{
    public abstract class AbstractBonusView : MonoBehaviour
    {
        public abstract void EnableView();
        public abstract void DisableView();
        public abstract void Initialize(IBonusAction action);
        public abstract void DeInitialize();
        public abstract void PerformBonus(GameId callerId);
    }
}