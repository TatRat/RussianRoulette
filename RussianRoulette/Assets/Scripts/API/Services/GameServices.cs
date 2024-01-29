using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace TatRat.API
{
    [UsedImplicitly]
    public sealed class GameServices : IDisposable
    {
        private static GameServices _instance;
        private Dictionary<Type, IService> _servicesMap;

        private static readonly Type[] _typeBlacklist =
        {
            typeof(IService)
        };

        public GameServices(IReadOnlyList<IService> services)
        {
            _servicesMap = new Dictionary<Type, IService>();

            foreach (var service in services)
            {
                var type = service.GetType();
                var interfaceTypes = type.GetInterfaces().Except(_typeBlacklist);

                if (!interfaceTypes.Any())
                {
                    throw new ArgumentException(
                        $"{type.Name} is concrete implementation of {nameof(IService)}, it's forbidden");
                }

                foreach (var interfaceType in interfaceTypes)
                {
                    if (!_servicesMap.TryAdd(interfaceType, service))
                    {
                        throw new ArgumentException(
                            $"Multiple implementations of service interface is forbidden. Trying register {type.Name} as {interfaceType.Name}");
                    }
                }
            }

            _instance = this;
        }

        public static TService GetService<TService>() where TService : class, IService
        {
            if (!_instance._servicesMap.TryGetValue(typeof(TService), out var result))
            {
                throw new ArgumentException($"No registered service of type {typeof(TService).Name}");
            }

            return result as TService;
        }

        public void Dispose()
        {
            _servicesMap.Clear();
            _instance = null;
        }
    }
}