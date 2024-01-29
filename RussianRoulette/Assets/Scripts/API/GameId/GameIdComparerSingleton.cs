using System;
using System.Linq;

namespace TatRat.API
{
    public static class GameIdComparerSingleton
    {
        private static IGameIdIndexComparer _indexComparer;
        private static IGameIdNameComparer _nameComparer;

        public static IGameIdIndexComparer IndexComparer
            => _indexComparer ??= GetInstance<IGameIdIndexComparer>();

        public static IGameIdNameComparer NameComparer
            => _nameComparer ??= GetInstance<IGameIdNameComparer>();

        private static T GetInstance<T>() where T : IGameIdComparer
        {
            var typeToCreate = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .FirstOrDefault(type =>
                    !type.IsInterface
                    && !type.IsAbstract
                    && type.GetInterfaces().Contains(typeof(T)));

            if (typeToCreate is null)
            {
                throw new Exception($"Classes implementing interface {typeof(T).Name} not found");
            }
            
            return (T)Activator.CreateInstance(typeToCreate);
        }
    }
}