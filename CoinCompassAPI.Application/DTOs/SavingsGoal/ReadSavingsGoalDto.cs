using System.ComponentModel.DataAnnotations;

namespace CoinCompassAPI.Application.DTOs.SavingsGoal
{
    public class ReadSavingsGoalDto
    {
        public string UserName { get; set; }
        public string Email {  get; set; }
        public string NomeMeta { get; set; }
        public decimal QuantiaObjetivo { get; set; }
        public decimal QuantiaAtual { get; set; }
        public DateTime DataObjetivo { get; set; }
    }
}
