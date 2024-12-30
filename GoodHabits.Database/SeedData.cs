using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public static class SeedData{
    public static void Seed(ModelBuilder modelBuilder){
        modelBuilder.Entity<Habit>().HasData(
            new Habit { Id = 1, Name = "Exercise", Description = "Do some exercise", TenantName = "CloudSphere" },
            new Habit { Id = 2, Name = "Read", Description = "Read a book", TenantName = "CloudSphere" },
            new Habit { Id = 3, Name = "Meditate", Description = "Meditate for 10 minutes", TenantName = "CloudSphere" }
        );
    }
}