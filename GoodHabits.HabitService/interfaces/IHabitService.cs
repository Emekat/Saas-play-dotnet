using System.Collections.Generic;
using GoodHabits.Database.Entities;

namespace GoodHabits.HabitService;
public interface IHabitService
{
    Task<Habit> CreateHabitAsync(string name, string description, bool IsCompleted = false);
    Task<Habit> GetHabitAsync(int habitId);
    Task<IReadOnlyList<Habit>> GetHabitsAsync();
}