namespace GoodHabits.Database;
public class TenantSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public List<Tenant> Tenants { get; set; } = new();
}
public class Tenant
{
    public string TenantName { get; set; } = default!;
    public string ConnectionString { get; set; } = default!;
}