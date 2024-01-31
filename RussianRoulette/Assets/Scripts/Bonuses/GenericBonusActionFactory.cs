using System;
using TatRat.API;

namespace TatRat.Bonuses
{
    public abstract class GenericBonusActionFactory<TAction, TData> : IBonusActionFactory
        where TAction : IBonusAction
        where TData : IBonusActionData
    {
        public Type GetActionDataType() => 
            typeof(TData);

        public IBonusAction Create(IBonusActionData input)
        {
            if (input is not TData typedData)
            {
                throw new ArgumentException(
                    $"Wrong action data type, expected {GetActionDataType().Name}, received: {input.GetType().Name}");
            }
            
            return CreateInternal(typedData);
        }

        protected abstract TAction CreateInternal(TData input);
    }
}