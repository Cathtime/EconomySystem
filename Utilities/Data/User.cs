namespace EconomySystem.Utilities.Data
{
    public class User(string name, int balance)
    {
        public string Name { get; set; } = name;
        private int Balance {get; set;} = balance;

        private DateTime workCooldown;

        public DateTime GetWorkCooldown()
        {
            return workCooldown;
        }

        public void SetCooldown(DateTime time)
        {
            workCooldown = time;
        }

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