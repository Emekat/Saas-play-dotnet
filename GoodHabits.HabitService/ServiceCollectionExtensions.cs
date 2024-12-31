using GoodHabits.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GoodHabits.HabitService;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAndMigrateDatabases(this IServiceCollection services)
    {
        var options = services.GetOptions<TenantSettings>(nameof(TenantSettings));
        var defaultConnectionString = options.DefaultConnectionString;

        services.AddDbContext<GoodHabitsDbContext>(opt =>
        {
            opt.UseNpgsql(e => e.MigrationsAssembly(typeof(GoodHabitsDbContext).Assembly.FullName));
            opt.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        });

        var tenants = options.Tenants;
        foreach (var tenant in tenants!)
        {
            string connectionString;
            if (string.IsNullOrEmpty(tenant.ConnectionString))
            {
                connectionString = defaultConnectionString;
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

    private static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var config = serviceProvider.GetRequiredService<IConfiguration>();
        var section = config.GetSection(sectionName);
        var options = new T();
        section.Bind(options);
        return options;
    }
}