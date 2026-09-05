using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Abstractions;
using TaskManagementSystem.Application.DTOs;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly ILogger<TasksController> _logger;

    public TasksController(ITaskService taskService, ILogger<TasksController> logger)
    {
        _taskService = taskService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetAll()
    {
        return Ok(await _taskService.GetAllTasksAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskDto>> GetById(string id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> Create([FromBody] CreateTaskDto task)
    {
        var created = await _taskService.CreateTaskAsync(task);
        _logger.LogInformation("API: created task id {TaskId}.", created.Id);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TaskDto>> Update(string id, [FromBody] UpdateTaskDto task)
    {
        return Ok(await _taskService.UpdateTaskAsync(id, task));
    }

    [HttpPatch("{id}/status")]
    public async Task<ActionResult<TaskDto>> UpdateStatus(string id, [FromBody] PatchTaskStatusDto patch)
    {
        return Ok(await _taskService.UpdateTaskStatusAsync(id, patch.Status));
    }

    [HttpPatch("{id}/priority")]
    public async Task<ActionResult<TaskDto>> UpdatePriority(string id, [FromBody] PatchTaskPriorityDto patch)
    {
        return Ok(await _taskService.UpdateTaskPriorityAsync(id, patch.Priority));
    }

    [HttpPatch("{id}/due-date")]
    public async Task<ActionResult<TaskDto>> UpdateDueDate(string id, [FromBody] PatchTaskDueDateDto patch)
    {
        return Ok(await _taskService.UpdateTaskDueDateAsync(id, patch.DueDate));
    }

    [HttpPatch("{id}/assignee")]
    public async Task<ActionResult<TaskDto>> Assign(string id, [FromBody] AssignTaskDto patch)
    {
        return Ok(await _taskService.AssignTaskAsync(id, patch.AssignedTo));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _taskService.DeleteTaskAsync(id);
        if (!deleted)
        {
            _logger.LogWarning("API: delete failed, task id '{TaskId}' not found.", id);
            return NotFound();
        }
        return NoContent();
    }
}
