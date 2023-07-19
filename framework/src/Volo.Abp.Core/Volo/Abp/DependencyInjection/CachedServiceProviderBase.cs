using System;
using System.Collections.Concurrent;

namespace Volo.Abp.DependencyInjection;

public abstract class CachedServiceProviderBase : ICachedServiceProviderBase
{
    protected IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// STUDYCODE:Lazy<T> 类是一种延迟加载机制，它允许你在需要时才初始化对象或执行代码
    /// </summary>
    protected ConcurrentDictionary<Type, Lazy<object?>> CachedServices { get; }

    protected CachedServiceProviderBase(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        CachedServices = new ConcurrentDictionary<Type, Lazy<object?>>();
        CachedServices.TryAdd(typeof(IServiceProvider), new Lazy<object?>(() => ServiceProvider));
    }

    public virtual object? GetService(Type serviceType)
    {
        return CachedServices.GetOrAdd(
            serviceType,
            _ => new Lazy<object?>(() => ServiceProvider.GetService(serviceType))
        ).Value;
    }
    
    public T GetService<T>(T defaultValue)
    {
        return (T)GetService(typeof(T), defaultValue!);
    }

    public object GetService(Type serviceType, object defaultValue)
    {
        return GetService(serviceType) ?? defaultValue;
    }

    public T GetService<T>(Func<IServiceProvider, object> factory)
    {
        return (T)GetService(typeof(T), factory);
    }
    
    public object GetService(Type serviceType, Func<IServiceProvider, object> factory)
    {
        return CachedServices.GetOrAdd(
            serviceType,
            _ => new Lazy<object?>(() => factory(ServiceProvider))
        ).Value!;
    }
}
