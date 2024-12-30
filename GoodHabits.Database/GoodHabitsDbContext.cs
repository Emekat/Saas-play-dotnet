using GoodHabits.Database.Entities;
using GoodHabits.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

public class GoodHabitsDbContext : DbContext
{
    private readonly ITenantService _tenantService;
    public GoodHabitsDbContext(DbContextOptions<GoodHabitsDbContext> options, ITenantService tenantService) : base(options)
       => _tenantService = tenantService;

    public string TenantName => _tenantService.GetTenant()?.TenantName ?? String.Empty;
    public DbSet<Habit>? Habits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var tenantConfigurationString =  _tenantService.GetConnectionString();
        if (!string.IsNullOrEmpty(tenantConfigurationString))
        {
            optionsBuilder.UseNpgsql(_tenantService.GetConnectionString());
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //only correct tenant can access their data
        modelBuilder.Entity<Habit>().HasQueryFilter(a => a.TenantName == TenantName);
        SeedData.Seed(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.Entries<IHasTenant>().Where(entry => entry.State == EntityState.Added || entry.State == EntityState.Modified).ToList()
        .ForEach(entry => entry.Entity.TenantName = TenantName);

        return await base.SaveChangesAsync(cancellationToken);
    }
}