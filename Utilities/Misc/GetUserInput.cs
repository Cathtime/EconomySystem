namespace EconomySystem.Utilities.Misc
{
    public class InputHelper
    {
         public static string GetUserInput(string msg, string err)
        {
            string? input;

            Console.Write(msg); // outside so they see this only once
            do
            {
                input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input))
                {
                    return input; // should stop the loop all together
                }

                Console.Write(err);
            } while (true);
        }
    }
}