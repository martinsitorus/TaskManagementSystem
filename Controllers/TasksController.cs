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

    [HttpGet]
    public IActionResult GetAllTasks()
    {
        var tasks = _taskService.GetAllTasks();
        return Ok(tasks);
    }

    [HttpPost]
    public IActionResult CreateTask(TaskItem task)
    {
        var newTask = _taskService.CreateTask(task.Title, task.Description, task.AssignedTo, task.Priority, task.DueDate);
        return CreatedAtAction(nameof(GetAllTasks), new { id = newTask.Title }, newTask);
    }
}