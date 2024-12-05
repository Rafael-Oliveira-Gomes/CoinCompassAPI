namespace CoinCompassAPI.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        public int AccountId {  get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date {  get; set; }
        public string Description { get; set; }

        public Transaction() { }
        public Transaction(int accountId, string type, decimal amount, DateTime date, string descripition)
        {
            AccountId = accountId;
            Type = type;
            Amount = amount;
            Date = date;
            Description = descripition;

        }

        public void Update(int accountId, string type, decimal amount, DateTime date, string descripition)
        {
            AccountId = accountId;
            Type = type;
            Amount = amount;
            Date = date;
            Description = descripition;
        }
    }
}
