using System.Text.Json;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> CreateUser(string username, string userID)
    {
        if (await _userRepository.UserIDExists(userID) || await _userRepository.UsernameExists(username))
        {
            throw new ArgumentException("UserID or Username already exists.");
        }

        var user = new User(username, userID);
        await _userRepository.AddUser(user);
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
            return null;
        }

        var success = await _userRepository.UpdateUser(userID, newUsername);
        if (!success) return null;

        return await _userRepository.GetUserByUserID(userID);
    }

    public async Task<bool> DeleteUserByID(string userID)
    {
        return await _userRepository.DeleteUserByID(userID);
    }

    public async Task<bool> DeleteUserByUsername(string username)
    {
        return await _userRepository.DeleteUserByUsername(username);
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