using System.Text.Json;

public class UserRepository : IUserRepository
{
    private readonly string _filePath = "users.json";
    private List<User> _users;

    public UserRepository()
    {
        _users = LoadUsersFromFile();
    }

    private List<User> LoadUsersFromFile()
    {
        if (!File.Exists(_filePath))
        {
            return new List<User>();
        }

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
    }

    private void SaveUsersToFile()
    {
        var json = JsonSerializer.Serialize(_users);
        File.WriteAllText(_filePath, json);
    }

    public async Task<User?> GetUserByUserID(string userID)
    {
        return await Task.FromResult(_users.FirstOrDefault(u => u.UserID == userID));
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        return await Task.FromResult(_users.FirstOrDefault(u => u.Username == username));
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
        var user = _users.FirstOrDefault(u => u.Username == username);
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
        return await Task.FromResult(_users.Any(u => u.Username == username));
    }
}