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
    public IEnumerable<TaskItem> GetAllTasks()
    {
        var tasks = _taskService.GetAllTasks();
        return tasks;
    }

    [HttpPost("createtask")]
    public IActionResult CreateTask(TaskItem task)
    {
        var newTask = _taskService.CreateTask(task.Title, task.Description, task.AssignedTo, task.Priority, task.DueDate);
        return CreatedAtAction(nameof(GetAllTasks), new { id = newTask.Title }, newTask);
    }
    [HttpPut("updatetaskstatus")]
    public IActionResult UpdateTaskStatus(string taskId, string newStatus)
    {
        var task = _taskService.GetAllTasks().FirstOrDefault(t => t.Title == taskId);
        if (task == null)
        {
            return NotFound();
        }
        _taskService.UpdateTaskStatus(task, newStatus);
        return NoContent();
    }
    [HttpPut("updatetaskpriority")]
    public IActionResult UpdateTaskPriority(string taskId, string newPriority)
    {
        var task = _taskService.GetAllTasks().FirstOrDefault(t => t.Title == taskId);
        if (task == null)
        {
            return NotFound();
        }
        task.UpdatePriority(newPriority);
        return NoContent();
    }
    [HttpPut("updatetaskduedate")]
    public IActionResult UpdateTaskDueDate(string taskId, DateOnly newDueDate)
    {
        var task = _taskService.GetAllTasks().FirstOrDefault(t => t.Title == taskId);
        if (task == null)
        {
            return NotFound();
        }
        task.UpdateDueDate(newDueDate);
        return NoContent();
    }
    [HttpPut("updatetaskassignedto")]
    public IActionResult UpdateTaskAssignedTo(string taskId, User newAssignedTo)
    {
        var task = _taskService.GetAllTasks().FirstOrDefault(t => t.Title == taskId);
        if (task == null)
        {
            return NotFound();
        }
        task.UpdateAssignedTo(newAssignedTo);
        return NoContent();
    }
    [HttpDelete("deletetask")]
    public IActionResult DeleteTask(string taskId)
    {
        _taskService.DeleteTask(taskId);
        return NoContent();
    }
    [HttpGet("gettaskbyid")]
    public IActionResult GetTaskById(string taskId)
    {
        var task = _taskService.GetAllTasks().FirstOrDefault(t => t.Title == taskId);
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }
    [HttpGet("gettaskbyassignedto")]
    public IActionResult GetTaskByAssignedTo(string assignedTo)
    {
        var tasks = _taskService.GetAllTasks().Where(t => t.AssignedTo.Username == assignedTo).ToList();
        if (!tasks.Any())
        {
            return NotFound();
        }
        return Ok(tasks);
    }

}