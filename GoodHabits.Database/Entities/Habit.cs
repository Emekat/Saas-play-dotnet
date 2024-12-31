using GoodHabits.Database.Interfaces;

namespace GoodHabits.Database.Entities;

public record Habit : IHasTenant
{
    public int Id { get; set; }
    // Guid.CreateVersion7();
    public Guid AccountabilityPartnerId { get; set; } = Guid.Empty;

    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool IsCompleted { get; set; }
    public string TenantName { get; set; } = default!;
}