using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Abstractions;
using TaskManagementSystem.Application.DTOs;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITaskService _taskService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(
        IUserService userService,
        ITaskService taskService,
        ILogger<UsersController> logger)
    {
        _userService = userService;
        _taskService = taskService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll([FromQuery] string? username)
    {
        if (!string.IsNullOrEmpty(username))
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(new[] { user });
        }
        return Ok(await _userService.GetAllUsersAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound("User not found.");
        }
        return Ok(user);
    }

    [HttpGet("{id}/tasks")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks(string id)
    {
        if (!await UserExists(id))
        {
            return NotFound("User not found.");
        }
        return Ok(await _taskService.GetTasksByUserIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto user)
    {
        var created = await _userService.CreateUserAsync(user);
        _logger.LogInformation("API: created user '{Username}'.", created.Username);
        return CreatedAtAction(nameof(GetById), new { id = created.UserID }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> Update(string id, [FromBody] UpdateUserDto user)
    {
        var updated = await _userService.UpdateUserAsync(id, user);
        if (updated == null)
        {
            return NotFound("User not found.");
        }
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _userService.DeleteUserAsync(id);
        if (!deleted)
        {
            return NotFound("User not found.");
        }
        return NoContent();
    }

    private async Task<bool> UserExists(string id) =>
        await _userService.GetUserByIdAsync(id) != null;
}
