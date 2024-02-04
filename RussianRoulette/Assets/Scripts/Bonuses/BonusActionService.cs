using System;
using System.Collections.Generic;
using TatRat.API;

namespace TatRat.Bonuses
{
    public class BonusActionService : IBonusActionService
    {
        private readonly Dictionary<Type, IBonusActionFactory> _factories;

        public BonusActionService(IReadOnlyList<IBonusActionFactory> factories)
        {
            _factories = new Dictionary<Type, IBonusActionFactory>();
            
            foreach (IBonusActionFactory factory in factories) 
                _factories.Add(factory.GetActionDataType(), factory);
        }

        public IBonusAction GetBonusActionByData(IBonusActionData data)
        {
            if (!_factories.TryGetValue(data.GetType(), out IBonusActionFactory factory))
            {
                throw new ArgumentException(
                $"No registered Bonus Action factory for data with type: {data.GetType()}");
            }

            return factory.Create(data);
        }
    }
}