namespace CoinCompassAPI.Domain.Entities
{
    public class SavingsGoal
    {
        public int Id { get; set; }
        public string GoalName { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public DateTime TargetDate { get; set; }
        public string UserId { get; set; }
        public virtual Budget Budget { get; set; }
        public virtual ApplicationUser User { get; set; }


        public SavingsGoal() { }

        public SavingsGoal(string userId, string goalName, decimal targetAmount, decimal currentAmount, DateTime targetDate)
        {
            UserId = userId;
            GoalName = goalName;
            TargetAmount = targetAmount;
            CurrentAmount = currentAmount;
            TargetDate = targetDate;
        }

        public void Update(string goalName, decimal targetAmount, decimal currentAmount, DateTime targetDate)
        {
            GoalName = goalName;
            TargetAmount = targetAmount;
            CurrentAmount = currentAmount;
            TargetDate = targetDate;
        }
    }
}
