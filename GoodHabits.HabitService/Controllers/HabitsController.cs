using Microsoft.AspNetCore.Mvc;
namespace GoodHabits.HabitService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HabitsController : ControllerBase
{
  private readonly IHabitService _habitService;
  private readonly ILogger<HabitsController> _logger;
    public HabitsController(IHabitService habitService, ILogger<HabitsController> logger)
    {
        _habitService = habitService;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        try
        {
            var habit = await _habitService.GetHabitAsync(id);
            return Ok(habit);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting habit");
            return StatusCode(500, "Error getting habit");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var habits = await _habitService.GetHabitsAsync();
            return Ok(habits);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting habits");
            return StatusCode(500, "Error getting habits");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateHabitDto request)
    {
        try
        {
            var habit = await _habitService.CreateHabitAsync(request.Name, request.Description, request.IsCompleted);
            return Ok(habit);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating habit");
            return StatusCode(500, "Error creating habit");
        }
    }
}