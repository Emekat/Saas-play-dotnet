using Microsoft.Extensions.Options;
using GoodHabits.Database;
namespace GoodHabits.HabitService;

/// <summary>
///intercepts incoming HTTP request, checks for tenant name in the header, sets the tenant and connection string
///
/// </summary>
public class TenantService : ITenantService
{
    private readonly TenantSettings _tenantSettings;
    private HttpContext? _httpContext;
    private Tenant? _tenant;

    public TenantService(IOptions<TenantSettings> tenantSettings, IHttpContextAccessor httpContextAccessor)
    {
        _tenantSettings = tenantSettings.Value;
        _httpContext = httpContextAccessor.HttpContext;
        if (_httpContext != null)
        {
            if(_httpContext.Request.Headers.TryGetValue("X-TenantName", out var tenantName))
            {
                SetTenant(tenantName);
            }
            else
            {
                throw new Exception("Invalid Tenant!");
            }
            }
    }
    private void SetTenant(string tenantName)
    {
        //should come from a db or cache, not appsettings
        //inject a repository in constructor
        _tenant = _tenantSettings.Tenants.FirstOrDefault(t => t.TenantName == tenantName);
        if(_tenant == null)
        {
            throw new Exception("Invalid Tenant!");
        }
        if(string.IsNullOrWhiteSpace(_tenant.ConnectionString))
        {
            SetDefualtConnectionStringToCurrentTenant();
        }
    }

    private void SetDefualtConnectionStringToCurrentTenant()
    {
        if (_tenant != null)
        {
            _tenant.ConnectionString = _tenantSettings.ConnectionString;
        }
    }
    public string GetTenant()
    {
        return _tenant.TenantName;
    }
    public string GetConnectionString()
    {
        return _tenant.ConnectionString;
    }
}
