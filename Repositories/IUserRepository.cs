
public interface IUserRepository
{
    Task<User?> GetUserByUserID(string userID);
    Task<User?> GetUserByUsername(string username);
    Task<IEnumerable<User>> GetAllUsers();
    Task AddUser(User user);
    Task<bool> UpdateUser(string userID, string newUsername);
    Task<bool> DeleteUserByID(string userID);
    Task<bool> DeleteUserByUsername(string username);
    Task<bool> UserIDExists(string userID);
    Task<bool> UsernameExists(string username);
}