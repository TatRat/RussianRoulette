using System;

namespace TatRat.API
{
    public interface IBonusActionFactory : IFactory<IBonusAction, IBonusActionData>
    {
        public Type GetActionDataType();
    }
}