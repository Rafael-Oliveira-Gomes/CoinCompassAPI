using System.Globalization;

namespace CoinCompassAPI.Domain.Entities
{
    public class Budget
    {
        public int Id { get; set; }
        public string Category {  get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }


        public Budget() { }
        public Budget(string userId, string category, decimal amount, DateTime startDate, DateTime endDate)
        {
            UserId = userId;
            Category = category;
            Amount = amount;
            StartDate = startDate;
            EndDate = endDate;
        }

        public void Update(string category, decimal amount, DateTime startDate, DateTime endDate)
        {
            Category = category;
            Amount = amount;
            StartDate = startDate;
            EndDate = endDate;
        }

    }
}
