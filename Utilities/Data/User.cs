namespace EconomySystem.Utilities.Data
{
    public class User(string name)
    {
        public string Name { get; set; } = name;
        private int Balance {get; set;} = 0;

        public int GetBalance()
        {
            return Balance;
        }

        public void AddBalance(int amnt)
        {
            Balance += amnt;
        }
        public void RemoveBalance(int amnt)
        {
            if (amnt > Balance) {return;}

            Balance -= amnt;
        }
    }
}