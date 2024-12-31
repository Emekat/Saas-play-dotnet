namespace GoodHabits.HabitService.Dtos;

public record CreateHabitDto(string Name, string Description, bool IsCompleted = false);