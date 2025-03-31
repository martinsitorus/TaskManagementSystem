using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("authenticate")]
    public IActionResult Authenticate(string username, string password)
    {
        var user = _userService.Authenticate(username, password);
        if (user == null)
        {
            return Unauthorized("Invalid credentials");
        }
        return Ok(user);
    }
}