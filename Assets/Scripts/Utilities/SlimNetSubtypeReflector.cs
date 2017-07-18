using System;
using System.Collections.Generic;

public static class SlimNetSubTypeReflector
{
    public static List<Type> GetSubTypes<T>() where T : class
    {
        var types = new List<Type>();

        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            if (assembly.FullName.StartsWith("Ability"))
                continue;

            foreach (Type type in assembly.GetTypes())
            {
                if (!type.IsClass)
                    continue;

                if (type.IsAbstract)
                    continue;

                if (!type.IsSubclassOf(typeof(T)))
                    continue;

                types.Add(type);
            }
        }

        return types;
    }
}