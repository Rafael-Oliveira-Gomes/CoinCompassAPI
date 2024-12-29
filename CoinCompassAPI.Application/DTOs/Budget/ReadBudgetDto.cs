using System.ComponentModel.DataAnnotations;

namespace CoinCompassAPI.Application.DTOs.Budget
{
    public class ReadBudgetDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Categoria { get; set; }
        public decimal Quantia { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}
