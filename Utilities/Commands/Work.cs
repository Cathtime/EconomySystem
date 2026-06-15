using EconomySystem.Utilities.Data;

namespace EconomySystem.Utilities.Commands
{
    public class WorkCommands
    {
        private static readonly TimeSpan cooldownTime = TimeSpan.FromHours(2);
        public static void Work(User user)
        {

            TimeSpan elapsed = DateTime.UtcNow - user.GetWorkCooldown();
            TimeSpan timeLeft = cooldownTime - elapsed;

            
            if (elapsed < cooldownTime)
            {
                string hourIdentifier = timeLeft.Hours >= 2 ? "hours" : "hour";
                Console.WriteLine($"Come back in {timeLeft.Hours} {hourIdentifier}, {timeLeft.Minutes} minutes and {timeLeft.Seconds} seconds");
                return;
            } else
            {
                user.SetCooldown(DateTime.UtcNow);
            }
            
            int RandomNumber = Random.Shared.Next(100, 300);

            user.AddBalance(RandomNumber);

            Console.WriteLine($"You now have {user.GetBalance()} money!");
        }
    }
}