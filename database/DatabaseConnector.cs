using System;
using System.Collections.Generic;
using Godot;
using MySqlConnector;

public partial class DatabaseConnector : Node
{
	private string connection = "Server=localhost;Database=godot3dgame;User ID=root;Password=password;";
	public void ConnectToDatabase(int userId, int levelId, string timeTaken, int coins)
	{
		string query = @"
			INSERT INTO stats (user_id, level_id, time_taken, coins_collected)
			VALUES (@user_id, @level_id, @time_taken, @coins_collected);";

		using (var connection = new MySqlConnection(this.connection))
		{
			try
			{
				connection.Open();
				GD.Print("Connected to MySQL database!");

				using (MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@user_id", userId);
					command.Parameters.AddWithValue("@level_id", levelId);
					command.Parameters.AddWithValue("@time_taken", timeTaken);
					command.Parameters.AddWithValue("@coins_collected", coins);


					int rowsAffected = command.ExecuteNonQuery();
					GD.Print("Time inserted into database! Rows affected: " + rowsAffected);
					if (rowsAffected > 0)
					{
						GD.Print("Data inserted successfully.");
					}
					else
					{
						GD.PrintErr("No rows were affected. Data may not have been inserted.");
					}
				}

			}
			catch	(MySqlException ex)
			{
				GD.PrintErr("Error connecting to MySQL: " + ex.Message);

			}
		}
	}

	public List<(string username, string timeTaken, int coins)> GetLeaderboard()
	{
		var leaderboard = new List<(string, string, int)>();

		using (var connection = new MySqlConnection(this.connection))
		{
			try
			{
				connection.Open();
				GD.Print("Fetching Leaderboard...");
				
				String query = @"
					SELECT 
						IF(s.user_id = 0, 'Anon',
						IFNULL(u.username, 'Unknown')) AS display_name,
						s.time_taken, s.coins_collected
					FROM stats s
					LEFT JOIN users u ON s.user_id = u.user_id
					ORDER BY s.time_taken ASC;";

					using (MySqlCommand command = new MySqlCommand(query, connection))
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							string username = reader.GetString("display_name");
							//string username = reader.GetString("username");
							string timeTaken = reader.GetString("time_taken");
							int coinsCollected = reader.GetInt32("coins_collected");
							leaderboard.Add((username, timeTaken, coinsCollected));
						}
					}
				/* using (MySqlCommand command = new MySqlCommand("SELECT username, time_taken, coins_collected FROM stats ORDER BY time_taken ASC", connection))
				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						string username = reader.IsDBNull(reader.GetOrdinal("username")) ? "Unknown" : reader.GetString("username");
						string timeTaken = reader.GetString("time_taken");
						int coinsCollected = reader.GetInt32("coins_collected");
						leaderboard.Add((username, timeTaken, coinsCollected));
					}
				} */
			}
			catch	(MySqlException ex)
			{
				  GD.PrintErr("Error fetching leaderboard: " + ex.Message);
			}
		}

		return leaderboard;
		}

	}
