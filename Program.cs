using static EconomySystem.Utilities.Misc.InputHelper;
using EconomySystem.Utilities.Data;
using Npgsql;
using DotNetEnv;


namespace EconomySystem
{
    // Program.cs should only be for selecting and running commands
    // ask the user for their name
    // then load all the commands available to them

    class Program
    {
        static string Name = "";
        static int option = 0;
       async static Task Main()
        {
            Env.Load();

            Console.WriteLine($"HOST: [{Env.GetString("Host")}]");

            string connectionString =
                $"Host={Env.GetString("Host")};" +
                $"Port={Env.GetString("Port")};" +
                $"Database={Env.GetString("Database")};" +
                $"Username={Env.GetString("Username")};" +
                $"Password={Env.GetString("Password")};";

            using var connection = new NpgsqlConnection(connectionString);

            await connection.OpenAsync();

            Console.WriteLine("Connected!");

            // Made a method because having like a million while loops for inputs would get wonky
            Name = GetUserInput(
                "Please enter your name: ",
                "Enter a valid Name: "
            );

            User user = new(Name);

            // this is for int validation
            do
            {
                // this is for string validation
                option = int.Parse(GetUserInput(
                "Please enter options 1-3 ",
                "Please enter options 1-3 "
            ));
            } while(option > 3 || option <= 0);

            switch (option)
            {
                case 1:
                    {
                        break;
                    }
                case 2:
                    {
                        break;
                    }
                case 3:
                        break;
                default:
                    {
                        break;
                    }
            }
        }
        
    }
}