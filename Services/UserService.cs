
public class UserService : IUserService
{
    private readonly List<User> _users = new()
    {

    };

    public bool TryAuthenticate(string username, string password, out User? user)
    {
        user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);
        return user != null;
    }

    // if (userService.TryAuthenticate(username, password, out var user))
    // {
    //     Console.WriteLine($"Welcome, {user.Username}!");
    // }
    // else
    // {
    //     Console.WriteLine("Invalid credentials.");
    // }

    public Task<User> Create(User user, string password)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task Update(User user, string password)
    {
        throw new NotImplementedException();
    }

    Task<User> IUserService.Authenticate(string username, string password)
    {
        throw new NotImplementedException();
    }
}