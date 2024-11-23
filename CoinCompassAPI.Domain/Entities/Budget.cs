using System.Globalization;

namespace CoinCompassAPI.Domain.Entities
{
    public class Budget
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Category {  get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
