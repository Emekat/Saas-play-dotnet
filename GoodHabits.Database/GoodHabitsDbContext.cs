using Microsoft.EntityFrameworkCore;

public class GoodHabitsDbContext : DbContext
{
    public DbSet<Habit>? Habits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;PORT=5432;Database=mydb;Username=postgres;Password=password;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Habit>().HasData(
            new Habit { Id = 1, Name = "Exercise", Description = "Do some exercise" },
            new Habit { Id = 2, Name = "Read", Description = "Read a book" },
            new Habit { Id = 3, Name = "Meditate", Description = "Meditate for 10 minutes" },
            new Habit { Id = 4, Name = "Write", Description = "Write a blog post" }
        );
    }
}