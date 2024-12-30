public record Habit
{
    public int Id { get; set; }
 
    // Guid.CreateVersion7();
    public Guid AccountabilityPartnerId { get; set; } = Guid.Empty;

    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}