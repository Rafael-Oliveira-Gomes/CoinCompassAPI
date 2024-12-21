namespace CoinCompassAPI.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public string BankName { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public Account() { }

        public Account(string userId, string accountType, decimal balance, string bankName)
        {
            UserId = userId;
            AccountType = accountType;
            Balance = balance;
            BankName = bankName;
        }

        public void Update(string accountType, decimal balance, string bankName)
        {
            AccountType = accountType;
            Balance = balance;
            BankName = bankName;
        }
    }
}
