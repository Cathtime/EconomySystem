using Npgsql;
using static EconomySystem.Utilities.Misc.InputHelper;

namespace EconomySystem.Utilities.Data
{
    public class UserRepo
    {
        // yes I'm aware this login system is horrid
        // this is just so I get comfortable with sql and Nqgsql
        async public static Task<User?> FindUser(NpgsqlConnection connection)
        { 
            string Username = GetUserInput("Enter your username: ", "Enter a valid username: ");
            string Password = GetUserInput("Enter your password: ", "Enter a valid password: ");

            string sql = """
                            SELECT "username", "password", "balance"
                            FROM "Users"
                            WHERE "username" = @username
                            AND "password" = @password
                            """;

            using var cmd = new NpgsqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("username", Username);
            cmd.Parameters.AddWithValue("password", Password);

            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                User user = new(
                    reader.GetString(reader.GetOrdinal("Username")), 
                    reader.GetInt32(reader.GetOrdinal("balance"))
                );

                Console.WriteLine("Login success");
                return user;
            }
            else
            {
                Console.WriteLine("Invalid login");
                return null;
            }
        }

        async public static Task<User?> CreateUser(NpgsqlConnection connection)
        {
            string Username = GetUserInput("Enter your username: ", "Enter a valid username: ");
            string Password = GetUserInput("Enter your password: ", "Enter a valid password: ");

            string sql = """
                            INSERT INTO "Users" (username, balance, password)
                            VALUES 
                            (@username, @balance, @password)

                            """;

            using var cmd = new NpgsqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("username", Username);
            cmd.Parameters.AddWithValue("password", Password);
            cmd.Parameters.AddWithValue("balance", 0);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();

            if (rowsAffected > 0)
            {
                Console.WriteLine("Account created successfully");

                return new User(
                    Username,
                    0
                );
            }

            return null;
        }
    }
}