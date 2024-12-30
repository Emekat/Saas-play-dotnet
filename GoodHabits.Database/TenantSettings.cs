namespace GoodHabits.Database;
public class TenantSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public List<string> Tenants { get; set; } = new();
}
public class Tenant
{
    public string Name { get; set; } = default!;
    public string ConnectionString { get; set; } = default!;
}