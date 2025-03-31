public interface IUserService
{
    Task<User> CreateUser(string username, string userID);
    Task<User> GetUserByUserID(string userID);
    Task<User> GetUserByUsername(string username);
    // get all users
    Task<List<User>> GetAllUsers();
    // update user
    Task<User> UpdateUser(string userID, string newUsername);
    // delete user
    Task<bool> DeleteUserByID(string userID);
    Task<bool> DeleteUserByUsername(string userID);
    // check if user exists
    Task<bool> UserIDExists(string userID);
    // check if username exists
    Task<bool> UsernameExists(string username);
}