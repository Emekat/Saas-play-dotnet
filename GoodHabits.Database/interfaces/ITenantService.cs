namespace GoodHabits.Database.Interfaces;
public interface ITenantService
{
    public Tenant? GetTenant();
    public string GetConnectionString();
}