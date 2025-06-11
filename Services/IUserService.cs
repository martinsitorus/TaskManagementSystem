public interface IUserService
{
    // Create a new user
    Task<User> CreateUser(string username, string userID);

    // Get a user by their UserID
    Task<User?> GetUserByUserID(string userID);

    // Get a user by their Username
    Task<User?> GetUserByUsername(string username);

    // Get all users
    Task<IEnumerable<User>> GetAllUsers();

    // Update a user's username
    Task<User?> UpdateUser(string userID, string newUsername);

    // Delete a user by their UserID
    Task<bool> DeleteUserByID(string userID);

    // Delete a user by their Username
    Task<bool> DeleteUserByUsername(string username);

    // Check if a UserID exists
    Task<bool> UserIDExists(string userID);

    // Check if a Username exists
    Task<bool> UsernameExists(string username);
}