using System.Text.Json;

public class UserRepository : IUserRepository
{
    private readonly string _filePath;
    private readonly List<User> _users;

    public UserRepository(string? filePath = null)
    {
        _filePath = filePath ?? "./Data/users.json";
        _users = LoadUsersFromFile();
    }

    private List<User> LoadUsersFromFile()
    {
        if (!File.Exists(_filePath))
        {
            return new List<User>();
        }

        try
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }
        catch (JsonException)
        {
            // Corrupt seed/data file: start empty rather than crashing the app.
            return new List<User>();
        }
    }

    private void SaveUsersToFile()
    {
        var directory = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }
        var json = JsonSerializer.Serialize(_users);
        File.WriteAllText(_filePath, json);
    }

    public async Task<User?> GetUserByUserID(string userID)
    {
        return await Task.FromResult(_users.FirstOrDefault(u => u.UserID == userID));
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        return await Task.FromResult(
            _users.FirstOrDefault(u => string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase)));
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await Task.FromResult(_users);
    }

    public async Task AddUser(User user)
    {
        _users.Add(user);
        SaveUsersToFile();
        await Task.CompletedTask;
    }

    public async Task<bool> UpdateUser(string userID, string newUsername)
    {
        var user = _users.FirstOrDefault(u => u.UserID == userID);
        if (user == null) return false;

        user.Username = newUsername;
        SaveUsersToFile();
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteUserByID(string userID)
    {
        var user = _users.FirstOrDefault(u => u.UserID == userID);
        if (user == null) return false;

        _users.Remove(user);
        SaveUsersToFile();
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteUserByUsername(string username)
    {
        var user = _users.FirstOrDefault(u => string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase));
        if (user == null) return false;

        _users.Remove(user);
        SaveUsersToFile();
        return await Task.FromResult(true);
    }

    public async Task<bool> UserIDExists(string userID)
    {
        return await Task.FromResult(_users.Any(u => u.UserID == userID));
    }

    public async Task<bool> UsernameExists(string username)
    {
        return await Task.FromResult(
            _users.Any(u => string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase)));
    }
}
