using Godot;

public partial class UserState : Node
{
    public int UserId {get; private set;}
    public string Username {get; private set;}
    public bool isLoggedIn => UserId != 0;  // Assuming 0 means not logged in

    public void Login(int userId, string username)
    {
        UserId = userId;
        Username = username;
        GD.Print($"User {Username} logged in.");
    }

    public void Logout()
    {
        UserId = 0;
        Username = string.Empty;
        GD.Print("User logged out.");
    }
}
