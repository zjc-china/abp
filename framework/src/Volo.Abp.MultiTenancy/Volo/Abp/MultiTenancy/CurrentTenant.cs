using System;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp.MultiTenancy;

public class CurrentTenant : ICurrentTenant, ITransientDependency
{
    public virtual bool IsAvailable => Id.HasValue;

    public virtual Guid? Id => _currentTenantAccessor.Current?.TenantId;

    public string? Name => _currentTenantAccessor.Current?.Name;

    private readonly ICurrentTenantAccessor _currentTenantAccessor;

    public CurrentTenant(ICurrentTenantAccessor currentTenantAccessor)
    {
        _currentTenantAccessor = currentTenantAccessor;
    }

    public IDisposable Change(Guid? id, string? name = null)
    {
        return SetCurrent(id, name);
    }

    // STUDYCODE: 注意此处的处理 再一个Using Scope内 修改租户，在此Scope完成Dispose时回到之前租户
    private IDisposable SetCurrent(Guid? tenantId, string? name = null)
    {
        var parentScope = _currentTenantAccessor.Current;
        _currentTenantAccessor.Current = new BasicTenantInfo(tenantId, name);

        return new DisposeAction<ValueTuple<ICurrentTenantAccessor, BasicTenantInfo?>>(static (state) =>
        {
            var (currentTenantAccessor, parentScope) = state;
            currentTenantAccessor.Current = parentScope;
        }, (_currentTenantAccessor, parentScope));
    }
}
