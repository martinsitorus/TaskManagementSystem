public class User
{
    public string Username { get; set; }
    public string UserID { get; set; }

    public User(string username, string userID)
    {
        Username = username;
        UserID = userID;
    }
}
