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
       // static string Name = "";
        static int option = 0;        
       async static Task Main()
        {
            // load the env
            Env.Load();

            // connect to the DB
            string connectionString =
                $"Host={Env.GetString("Host")};" +
                $"Port={Env.GetString("Port")};" +
                $"Database={Env.GetString("Database")};" +
                $"Username={Env.GetString("Username")};" +
                $"Password={Env.GetString("Password")};";

            using var connection = new NpgsqlConnection(connectionString);

            await connection.OpenAsync();

            Console.WriteLine("Connected!");

            // prompt user to login or create new account
            do
            {
                option = int.Parse(GetUserInput(
                "1. Login. 2. New account ",
                "1. Login. 2. New account "
            ));
            } while(option > 2 || option <= 0);

            User? currentUser = null;
            do {
                
            currentUser = option switch
            {
                1 => await UserRepo.FindUser(connection),
                2 => await UserRepo.CreateUser(connection),
                _ => null };
   
            } while(currentUser == null);


        }
        
    }
}