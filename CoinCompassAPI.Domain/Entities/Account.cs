namespace CoinCompassAPI.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public string BankName { get; set; }
    }
}
