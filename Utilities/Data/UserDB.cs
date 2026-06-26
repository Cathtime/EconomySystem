using Npgsql;
using static EconomySystem.Utilities.Misc.InputHelper;

namespace EconomySystem.Utilities.Data
{
    public class UserRepo
    {
        // yes I'm aware this login system is horrid
        // this is just so I get comfortable with sql and Nqgsql

        // This method will find a user in the database based on their username and password
        // If the user is found, it will return a User object, otherwise it will return null
        async public static Task<User?> FindUser(NpgsqlConnection connection)
        { 
            string Username = GetUserInput("Enter your username: ", "Enter a valid username: ");
            string Password = GetUserInput("Enter your password: ", "Enter a valid password: ");

            // grabs username, password, and balance from the Users table
            // ONLY if the username and password match the inputted values
            string sql = """
                            SELECT "username", "password", "balance"
                            FROM "Users"
                            WHERE "username" = @username
                            AND "password" = @password
                            """;

            using var cmd = new NpgsqlCommand(sql, connection); // create a new command with the sql and connection

            // add the parameters to the command to prevent SQL injection
            cmd.Parameters.AddWithValue("username", Username);
            cmd.Parameters.AddWithValue("password", Password);

            using var reader = await cmd.ExecuteReaderAsync();

            // if the reader has a row, then the user was found
            if (await reader.ReadAsync())
            {
                // create a new user object with the username and balance from the database
                User user = new(
                    reader.GetString(reader.GetOrdinal("Username")), 
                    reader.GetInt32(reader.GetOrdinal("balance"))
                );

                // log the success 
                Console.WriteLine("Login success");
                return user;
            }
            else // and also the failure
            {
                Console.WriteLine("Invalid login");
                return null;
            }
        }

        // This method will create a new user in the database with the given username and password
        // If the user is created successfully, it will return a User object
        // we also return null BUT in the main file, we keep running these until one returns an actual user object
        // (you can't use the program without having data)
        async public static Task<User?> CreateUser(NpgsqlConnection connection)
        {
            string Username = GetUserInput("Enter your username: ", "Enter a valid username: ");
            string Password = GetUserInput("Enter your password: ", "Enter a valid password: ");

            // insert the new user into the Users table with a balance of 0 and the given username and password
            string sql = """
                            INSERT INTO "Users" (username, balance, password)
                            VALUES 
                            (@username, @balance, @password)

                            """;
            // sql connection made
            using var cmd = new NpgsqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("username", Username);
            cmd.Parameters.AddWithValue("password", Password);
            cmd.Parameters.AddWithValue("balance", 0);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();

            if (rowsAffected > 0)
            {
                Console.WriteLine("Account created successfully");

                // return a user object
                return new User(
                    Username,
                    0
                );
            }

            // this is more a safeguard in case something errors like when inserting values to the db
            return null; // if the user was not created, return null
        }
    }
}