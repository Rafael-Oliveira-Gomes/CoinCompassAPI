namespace CoinCompassAPI.Domain.Entities
{
    public class SavingsGoal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string GoalName { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public DateTime TargetDate { get; set; }


        public SavingsGoal(int userId, string goalName, decimal targetAmount, decimal currentAmount, DateTime targetDate)
        {
            UserId = userId;
            GoalName = goalName;
            TargetAmount = targetAmount;
            CurrentAmount = currentAmount;
            TargetDate = targetDate;
        }

        public void Update(int userId, string goalName, decimal targetAmount, decimal currentAmount, DateTime targetDate)
        {
            UserId = userId;
            GoalName = goalName;
            TargetAmount = targetAmount;
            CurrentAmount = currentAmount;
            TargetDate = targetDate;
        }
    }
}
