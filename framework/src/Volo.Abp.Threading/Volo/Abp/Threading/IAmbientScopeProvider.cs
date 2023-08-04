using System;

namespace Volo.Abp.Threading;

/// <summary>
/// 环境作用域提供器
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IAmbientScopeProvider<T>
{
    T? GetValue(string contextKey);

    IDisposable BeginScope(string contextKey, T value);
}
