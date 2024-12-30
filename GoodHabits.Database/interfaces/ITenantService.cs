using GoodHabits.Database;

public interface ITenantService
{
    public Tenant GetTenant();
    public string GetConnectionString();
}