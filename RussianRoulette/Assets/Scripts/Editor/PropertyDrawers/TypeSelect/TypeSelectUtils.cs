using System;
using System.Linq;

namespace TatRat.Editor.TypeSelect
{
    internal static class TypeSelectUtils
    {
        public static Type[] GetAllowedTypes(Type fieldType)
        {
            var baseType = fieldType;

            if (fieldType.IsArray)
            {
                baseType = fieldType.GetElementType();
            }
            else if (fieldType.IsGenericType)
            {
                baseType = fieldType.GetGenericArguments().Single();
            }

            var isBaseTypeInterface = baseType.IsInterface;
            var allowedTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => !type.IsInterface
                               && !type.IsAbstract
                               && isBaseTypeInterface
                    ? type.GetInterfaces().Contains(baseType)
                    : type.IsSubclassOf(baseType))
                .ToArray();

            return allowedTypes;
        }

        public static Type GetTypeByFullName(string typeFullName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var type = assembly.GetType(typeFullName, false);
                if (type is not null)
                {
                    return type;
                }
            }

            return null;
        }

        public static bool IsTypeSubclassOrImplementingInterface(Type targetType, Type checkType)
            => checkType.IsInterface
                ? targetType.GetInterfaces().Contains(checkType)
                : targetType.IsSubclassOf(checkType);
    }
}