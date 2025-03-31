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

    [HttpPost("createUser")]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.UserID))
        {
            return BadRequest("Invalid user data.");
        }

        var createdUser = await _userService.CreateUser(user.Username, user.UserID);
        return CreatedAtAction(nameof(GetUserByUserID), new { userID = createdUser.UserID }, createdUser);
    }
    
    [HttpGet("getUserByUserID/{userID}")]
    public async Task<IActionResult> GetUserByUserID(string userID)
    {
        var user = await _userService.GetUserByUserID(userID);
        if (user == null)
        {
            return NotFound("User not found.");
        }
        return Ok(user);
    }
    [HttpGet("getUserByUsername/{username}")]
    public async Task<IActionResult> GetUserByUsername(string username)
    {
        var user = await _userService.GetUserByUsername(username);
        if (user == null)
        {
            return NotFound("User not found.");
        }
        return Ok(user);
    }
    [HttpGet("getAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        return Ok(users);
    }
    [HttpPut("updateUser/{userID}")]
    public async Task<IActionResult> UpdateUser(string userID, [FromBody] string newUsername)
    {
        if (string.IsNullOrEmpty(newUsername))
        {
            return BadRequest("Invalid username.");
        }

        var updatedUser = await _userService.UpdateUser(userID, newUsername);
        if (updatedUser == null)
        {
            return NotFound("User not found.");
        }
        return Ok(updatedUser);
    }
    [HttpDelete("deleteUserByID/{userID}")]
    public async Task<IActionResult> DeleteUserByID(string userID)
    {
        var deleted = await _userService.DeleteUserByID(userID);
        if (!deleted)
        {
            return NotFound("User not found.");
        }
        return NoContent();
    }
    [HttpDelete("deleteUserByUsername/{username}")]
    public async Task<IActionResult> DeleteUserByUsername(string username)
    {
        var deleted = await _userService.DeleteUserByUsername(username);
        if (!deleted)
        {
            return NotFound("User not found.");
        }
        return NoContent();
    }
    [HttpGet("userIDExists/{userID}")]
    public async Task<IActionResult> UserIDExists(string userID)
    {
        var exists = await _userService.UserIDExists(userID);
        return Ok(new { exists });
    }
    [HttpGet("usernameExists/{username}")]
    public async Task<IActionResult> UsernameExists(string username)
    {
        var exists = await _userService.UsernameExists(username);
        return Ok(new { exists });
    }
}