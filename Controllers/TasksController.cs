using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly ILogger<TasksController> _logger;

    public TasksController(ITaskService taskService, ILogger<TasksController> logger)
    {
        _taskService = taskService;
        _logger = logger;
    }

    [HttpGet("getalltasks")]
    public async Task<IEnumerable<TaskItem>> GetAllTasks()
    {
        var tasks = await _taskService.GetAllTasks();
        return tasks;
    }

    [HttpPost("createtask")]
    public async Task<IActionResult> CreateTask(TaskItem task)
    {
        var newTask = await _taskService.CreateTask(task.Title, task.Description, task.AssignedTo, task.Priority, task.DueDate);
        _logger.LogInformation("API: created task '{Title}'.", newTask.Title);
        return CreatedAtAction(nameof(GetTaskById), new { taskId = newTask.Title }, newTask);
    }

    [HttpPut("updatetaskstatus")]
    public async Task<IActionResult> UpdateTaskStatus(string taskId, string newStatus)
    {
        var task = await _taskService.GetTaskByTitle(taskId);
        if (task == null)
        {
            return NotFound();
        }
        await _taskService.UpdateTask(taskId, newStatus, task.Priority, task.DueDate, task.AssignedTo);
        return NoContent();
    }

    [HttpPut("updatetaskpriority")]
    public async Task<IActionResult> UpdateTaskPriority(string taskId, string newPriority)
    {
        var task = await _taskService.GetTaskByTitle(taskId);
        if (task == null)
        {
            return NotFound();
        }
        await _taskService.UpdateTask(taskId, task.Status, newPriority, task.DueDate, task.AssignedTo);
        return NoContent();
    }

    [HttpPut("updatetaskduedate")]
    public async Task<IActionResult> UpdateTaskDueDate(string taskId, DateOnly newDueDate)
    {
        var task = await _taskService.GetTaskByTitle(taskId);
        if (task == null)
        {
            return NotFound();
        }
        await _taskService.UpdateTask(taskId, task.Status, task.Priority, newDueDate, task.AssignedTo);
        return NoContent();
    }

    [HttpPut("updatetaskassignedto")]
    public async Task<IActionResult> UpdateTaskAssignedTo(string taskId, User newAssignedTo)
    {
        var task = await _taskService.GetTaskByTitle(taskId);
        if (task == null)
        {
            return NotFound();
        }
        await _taskService.UpdateTask(taskId, task.Status, task.Priority, task.DueDate, newAssignedTo);
        return NoContent();
    }

    [HttpDelete("deletetask")]
    public async Task<IActionResult> DeleteTask(string taskId)
    {
        var deleted = await _taskService.DeleteTask(taskId);
        if (!deleted)
        {
            _logger.LogWarning("API: delete failed, task '{Title}' not found.", taskId);
            return NotFound();
        }
        return NoContent();
    }

    [HttpGet("gettaskbyid")]
    public async Task<IActionResult> GetTaskById(string taskId)
    {
        var task = await _taskService.GetTaskByTitle(taskId);
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    [HttpGet("gettaskbyassignedto")]
    public async Task<IActionResult> GetTaskByAssignedTo(string assignedTo)
    {
        var filtered = (await _taskService.GetTasksByUsername(assignedTo)).ToList();
        if (!filtered.Any())
        {
            return NotFound();
        }
        return Ok(filtered);
    }
}
