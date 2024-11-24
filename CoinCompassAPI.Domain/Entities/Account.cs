namespace CoinCompassAPI.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public string BankName { get; set; }

        public Account(int userId, string accountType, decimal balance, string bankName)
        {
            UserId = userId;
            AccountType = accountType;
            Balance = balance;
            BankName = bankName;
        }

        public void Update(int userId, string accountType, decimal balance, string bankName)
        {
            UserId = userId;
            AccountType = accountType;
            Balance = balance;
            BankName = bankName;
        }
    }
}
