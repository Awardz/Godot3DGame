using Godot;
using MySqlConnector;
using System;
using BCrypt.Net;


public partial class UserAuthentication : Node
{
    private string connection = "Server=localhost;Database=godot3dgame;User ID=root;Password=password;";

    public bool Register(string username, string password)
    {
        
        string hash  = BCrypt.Net.BCrypt.HashPassword(password);
        string query = "INSERT INTO users (username, password) VALUES (@username, @password);";

        using (var connection = new MySqlConnection(this.connection))
        {
            try
            {
                connection.Open();
                GD.Print("Connected to MySQL database!");

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", hash);
                    command.ExecuteNonQuery();
                    GD.Print("User registered!");
                }

                return true;
            }
            catch (MySqlException ex)
            {
                GD.PrintErr("Error connecting to MySQL: " + ex.Message);
                return false;
            }
        }
      
    }

    public (bool success, int userId) Login(string username, string password)
    {
        string query = "SELECT user_id, password FROM users WHERE username = @username;";

        using (var connection = new MySqlConnection(this.connection))
        {
            try
            {
                connection.Open();
                GD.Print("Connected to MySQL database!");
                

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            GD.Print("User found!");    
                            string hash = reader.GetString("password");
                            if (BCrypt.Net.BCrypt.Verify(password, hash))
                            {
                                int userId = reader.GetInt32("user_id");
                                GD.Print("User ID: " + userId);
                                
                         
                                GD.Print("User logged in!");
                                return (true, userId);
                            }
                            else
                            {
                                GD.Print("Invalid password.");
                                return (false, 0);
                            }
                        }
                    }
                }

                return (false, 0);
            }
            catch (MySqlException ex)
            {
                GD.PrintErr("Error connecting to MySQL: " + ex.Message);
                return (false, 0);
            }
        }
    }

}