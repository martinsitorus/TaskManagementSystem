using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
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
        return CreatedAtAction(nameof(GetAllTasks), new { id = newTask.Title }, newTask);
    }
    [HttpPut("updatetaskstatus")]
    public async Task<IActionResult> UpdateTaskStatus(string taskId, string newStatus)
    {
        var tasks = await _taskService.GetAllTasks();
        var task = tasks.FirstOrDefault(t => t.Title == taskId);
        if (task == null)
        {
            return NotFound();
        }
        // You should update via service, not directly on the entity
        await _taskService.UpdateTask(taskId, newStatus, task.Priority, task.DueDate, task.AssignedTo);
        return NoContent();
    }
    [HttpPut("updatetaskpriority")]
    public async Task<IActionResult> UpdateTaskPriority(string taskId, string newPriority)
    {
        var tasks = await _taskService.GetAllTasks();
        var task = tasks.FirstOrDefault(t => t.Title == taskId);
        if (task == null)
        {
            return NotFound();
        }
        task.UpdatePriority(newPriority);
        return NoContent();
    }
    [HttpPut("updatetaskduedate")]
    public async Task<IActionResult> UpdateTaskDueDate(string taskId, DateOnly newDueDate)
    {
        var tasks = await _taskService.GetAllTasks();
        var task = tasks.FirstOrDefault(t => t.Title == taskId);
        if (task == null)
        {
            return NotFound();
        }
        task.UpdateDueDate(newDueDate);
        return NoContent();
    }
    [HttpPut("updatetaskassignedto")]
    public async Task<IActionResult> UpdateTaskAssignedTo(string taskId, User newAssignedTo)
    {
        var tasks = await _taskService.GetAllTasks();
        var task = tasks.FirstOrDefault(t => t.Title == taskId);
        if (task == null)
        {
            return NotFound();
        }
        task.UpdateAssignedTo(newAssignedTo);
        return NoContent();
    }
    [HttpDelete("deletetask")]
    public async Task<IActionResult> DeleteTask(string taskId)
    {
        await _taskService.DeleteTask(taskId);
        return NoContent();
    }
    [HttpGet("gettaskbyid")]
    public async Task<IActionResult> GetTaskById(string taskId)
    {
        var tasks = await _taskService.GetAllTasks();
        var task = tasks.FirstOrDefault(t => t.Title == taskId);
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }
    [HttpGet("gettaskbyassignedto")]
    public async Task<IActionResult> GetTaskByAssignedTo(string assignedTo)
    {
        var tasks = await _taskService.GetAllTasks();
        var filtered = tasks.Where(t => t.AssignedTo.Username == assignedTo).ToList();
        if (!filtered.Any())
        {
            return NotFound();
        }
        return Ok(filtered);
    }

}