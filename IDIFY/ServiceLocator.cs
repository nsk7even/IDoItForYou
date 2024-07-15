using System;
using System.Collections.Concurrent;

namespace IDIFY;

public static class ServiceLocator
{
    private static readonly ConcurrentDictionary<Type, object> Services = new ConcurrentDictionary<Type, object>();
    
    public static void RegisterService<T>(T service)
    {
        if (service is null) return;
        
        Services.AddOrUpdate(service.GetType(), service, (_, _) => service);
    }

    public static T? GetService<T>()
    {
        if (Services.TryGetValue(typeof(T), out var service))
        {
            return (T)Convert.ChangeType(service, typeof(T));
        }

        return default;
    }
}