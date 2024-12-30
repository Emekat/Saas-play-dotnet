using GoodHabits.Database.Entities;
using Microsoft.EntityFrameworkCore;
public class HabitService : IHabitService
{
    private readonly GoodHabitsDbContext _dbContext;
    public HabitService(GoodHabitsDbContext dbContext) => _dbContext = dbContext;
    public async Task<Habit> CreateHabitAsync(string name, string description, bool IsCompleted = false)
    {
       var habit = _dbContext.Habits!.Add(new Habit{
              Name = name,
              Description = description,
              IsCompleted = IsCompleted
       }).Entity;
       await _dbContext.SaveChangesAsync();
       return habit;
    }

    public async Task<Habit> GetHabitAsync(int habitId)
     => await _dbContext.Habits!.FirstAsync(a => a.Id == habitId);

    public async Task<IReadOnlyList<Habit>> GetHabitsAsync()
     => await _dbContext.Habits!.ToListAsync();
}