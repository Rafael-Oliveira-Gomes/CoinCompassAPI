namespace CoinCompassAPI.Domain.Entities
{
    public class Outgoings
    {
        public int Id { get; set; }
        public int UserId {  get; set; }
        public string TypeOutgoings { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string HowPaid { get; set; }
        public decimal AmountOutGoings { get; set; }

    }
}
