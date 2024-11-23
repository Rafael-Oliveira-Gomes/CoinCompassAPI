namespace CoinCompassAPI.Domain.Entities
{
    public class Investment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string InvestmentType { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float InterestRate { get; set; }
    }
}
