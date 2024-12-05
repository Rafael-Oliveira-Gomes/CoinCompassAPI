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

        public Outgoings(int userId, string typeOutgoings, DateTime date, string description, string howPaid, decimal amountOutgoings)
        {
            UserId = userId;
            TypeOutgoings = typeOutgoings;
            Date = date;
            Description = description;
            HowPaid = howPaid;
            AmountOutGoings = amountOutgoings;

        }

        public void Update(int userId, string typeOutgoings, DateTime date, string description, string howPaid, decimal amountOutgoings)
        {
            UserId = userId;
            TypeOutgoings = typeOutgoings;
            Date = date;
            Description = description;
            HowPaid = howPaid;
            AmountOutGoings = amountOutgoings;
        }
    }
}
