using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public static class SeedData{
    public static void Seed(ModelBuilder modelBuilder){
        modelBuilder.Entity<Habit>().HasData(
            new Habit { Id = 1, Name = "Exercise", Description = "Do some exercise" },
            new Habit { Id = 2, Name = "Read", Description = "Read a book" },
            new Habit { Id = 3, Name = "Meditate", Description = "Meditate for 10 minutes" }
        );
    }
}