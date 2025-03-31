using System.Text.Json;

public class UserService : IUserService
{
    private readonly string _filePath = "./Data/users.json";
    private readonly List<User> _users = new List<User>();

    public UserService()
    {
        // Load users from JSON file at startup
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            var users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            _users.AddRange(users); // Add deserialized users to the existing list
        }
        else
        {
            _users = new List<User>();
        }
    }
    public void SaveUsersToFile()
    {
        var json = JsonSerializer.Serialize(_users);
        File.WriteAllText(_filePath, json);
    }
    public void LoadUsersFromFile()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            var users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            _users.Clear(); // Clear the existing list
            _users.AddRange(users); // Add deserialized users to the list
        }
        else
        {
            _users.Clear(); // Ensure the list is empty if the file doesn't exist
        }
    }
    public async Task<User> CreateUser(string username, string userID)
    {
        var user = new User(username, userID);
        _users.Add(user);
        SaveUsersToFile();
        return await Task.FromResult(user);
    }

    public async Task<User> GetUserByUserID(string userID)
    {
        LoadUsersFromFile(); // Load users from file before searching
        var user = _users.FirstOrDefault(u => u.UserID == userID) ?? throw new KeyNotFoundException($"User with the specified ID was not found.");
        return await Task.FromResult(user);
    }

    public async Task<User> GetUserByUsername(string username)
    {
        LoadUsersFromFile(); // Load users from file before searching
        var user = _users.FirstOrDefault(u => u.Username == username) ?? throw new KeyNotFoundException($"User with the specified Username was not found.");

        return await Task.FromResult(user);
    }
    public async Task<List<User>> GetAllUsers()
    {
        LoadUsersFromFile(); // Load users from file before returning
        if (_users.Count == 0)
        {
            throw new KeyNotFoundException("No users found.");
        }
        return await Task.FromResult(_users);
    }
    public async Task<User> UpdateUser(string userID, string newUsername)
    {
        LoadUsersFromFile(); // Load users from file before updating
        if (string.IsNullOrEmpty(newUsername))
        {
            throw new ArgumentException("New username cannot be null or empty.", nameof(newUsername));
        }
        if (string.IsNullOrEmpty(userID))
        {
            throw new ArgumentException("User ID cannot be null or empty.", nameof(userID));
        }
        var user = _users.FirstOrDefault(u => u.UserID == userID) ?? throw new KeyNotFoundException($"User with the specified ID was not found.");
        user.Username = newUsername;
        return await Task.FromResult(user);
    }
    public async Task<bool> DeleteUserByID(string userID)
    {
        LoadUsersFromFile();
        var user = _users.FirstOrDefault(u => u.UserID == userID);
        if (user != null)
        {
            _users.Remove(user);
            SaveUsersToFile();
            return await Task.FromResult(true);
        }
        return await Task.FromResult(false);
    }
    public async Task<bool> DeleteUserByUsername(string username)
    {
        LoadUsersFromFile(); // Load users from file before deleting
        var user = _users.FirstOrDefault(u => u.Username == username);
        if (user != null)
        {
            _users.Remove(user);
            SaveUsersToFile();
            return await Task.FromResult(true);
        }
        return await Task.FromResult(false);
    }
    public async Task<bool> UserIDExists(string userID)
    {
        LoadUsersFromFile(); // Load users from file before checking existence
        var exists = _users.Any(u => u.UserID == userID);
        return await Task.FromResult(exists);
    }
    public async Task<bool> UsernameExists(string username)
    {
        LoadUsersFromFile(); // Load users from file before checking existence
        var exists = _users.Any(u => u.Username == username);
        return await Task.FromResult(exists);
    }
}