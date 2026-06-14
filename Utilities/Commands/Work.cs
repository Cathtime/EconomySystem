using EconomySystem.Utilities.Data;

namespace EconomySystem.Utilities.Commands
{
    public class WorkCommands
    {
        // TODO: add cooldown
        public static void Work(User user)
        {
            int RandomNumber = Random.Shared.Next(100, 300);

            user.AddBalance(RandomNumber);

            Console.WriteLine($"You now have {user.GetBalance()} money!");
        }
    }
}