using GoodHabits.Database;
using Microsoft.EntityFrameworkCore;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAndMigrateDatabases(this IServiceCollection services,
    IConfiguration config)
    {
        var options = services.GetOptions<TenantSettings>(nameof(TenantSettings));
        var defaultConnectionString = options?.DefaultConnectionString;

        services.AddDbContextPool<GoodHabitsDbContext>(options =>
        {
            options.UseNpgsql(e => e.MigrationsAssembly(typeof(GoodHabitsDbContext).Assembly.FullName));
        });

        var tenants = options?.Tenants;
        foreach (var tenant in tenants!)
        {
            string connectionString = string.Empty;
            if (!string.IsNullOrEmpty(tenant.ConnectionString))
            {
                connectionString = defaultConnectionString!;
            }
            else
            {
                connectionString = tenant.ConnectionString;
            }

            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GoodHabitsDbContext>();

            dbContext.Database.SetConnectionString(connectionString);
            if(dbContext.Database.GetMigrations().Any())
            {
                dbContext.Database.Migrate();
            }
        }
        return services;
    }

    public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var config = serviceProvider.GetRequiredService<IConfiguration>();
        var section = config.GetSection(sectionName);
        var options = new T();
        section.Bind(options);
        return options;
    }
}