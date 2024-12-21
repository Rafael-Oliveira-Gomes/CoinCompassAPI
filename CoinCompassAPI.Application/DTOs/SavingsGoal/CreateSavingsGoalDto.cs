using System.ComponentModel.DataAnnotations;

namespace CoinCompassAPI.Application.DTOs.SavingsGoal
{
    public class CreateSavingsGoalDto
    {
        [Required]
        public string NomeMeta { get; set; }

        [Required]
        public decimal QuantiaObjetivo { get; set; }

        [Required]
        public decimal QuantiaAtual { get; set; }

        [Required]
        public DateTime DataObjetivo { get; set; }

    }
}
