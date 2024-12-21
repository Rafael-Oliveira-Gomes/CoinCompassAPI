using System.ComponentModel.DataAnnotations;

namespace CoinCompassAPI.Application.DTOs.Budget
{
    public class CreateBudgetDto
    {
        [Required]
        public string Categoria { get; set; }
        [Required]
        public decimal Quantia { get; set; }
        [Required]
        public DateTime DataInicio { get; set; }
        [Required]
        public DateTime DataFim { get; set; }

    }
}
