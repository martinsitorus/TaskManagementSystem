public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository userRepository, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<User> CreateUser(string username, string userID)
    {
        if (await _userRepository.UserIDExists(userID) || await _userRepository.UsernameExists(username))
        {
            _logger.LogWarning("Create user failed: UserID '{UserID}' or username '{Username}' already exists.", userID, username);
            throw new ArgumentException("UserID or Username already exists.");
        }

        var user = new User(username, userID);
        await _userRepository.AddUser(user);
        _logger.LogInformation("Created user '{Username}' (id {UserID}).", username, userID);
        return user;
    }

    public async Task<User?> GetUserByUserID(string userID)
    {
        return await _userRepository.GetUserByUserID(userID);
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        return await _userRepository.GetUserByUsername(username);
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _userRepository.GetAllUsers();
    }

    public async Task<User?> UpdateUser(string userID, string newUsername)
    {
        if (!await _userRepository.UserIDExists(userID))
        {
            _logger.LogWarning("Update failed: user id '{UserID}' not found.", userID);
            return null;
        }

        var success = await _userRepository.UpdateUser(userID, newUsername);
        if (!success) return null;

        _logger.LogInformation("Updated user id '{UserID}' to username '{Username}'.", userID, newUsername);
        return await _userRepository.GetUserByUserID(userID);
    }

    public async Task<bool> DeleteUserByID(string userID)
    {
        var deleted = await _userRepository.DeleteUserByID(userID);
        if (deleted)
        {
            _logger.LogInformation("Deleted user id '{UserID}'.", userID);
        }
        else
        {
            _logger.LogWarning("Delete failed: user id '{UserID}' not found.", userID);
        }
        return deleted;
    }

    public async Task<bool> DeleteUserByUsername(string username)
    {
        var deleted = await _userRepository.DeleteUserByUsername(username);
        if (deleted)
        {
            _logger.LogInformation("Deleted user '{Username}'.", username);
        }
        else
        {
            _logger.LogWarning("Delete failed: user '{Username}' not found.", username);
        }
        return deleted;
    }

    public async Task<bool> UserIDExists(string userID)
    {
        return await _userRepository.UserIDExists(userID);
    }

    public async Task<bool> UsernameExists(string username)
    {
        return await _userRepository.UsernameExists(username);
    }
}
