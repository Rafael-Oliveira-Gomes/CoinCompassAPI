using System.ComponentModel.DataAnnotations;

namespace CoinCompassAPI.Application.DTOs.Investment
{
    public class CreateInvestmentDto
    {

        [Required]
        public string TipoInvestimento { get; set; }

        [Required]
        public decimal Quantia { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        [Required]
        public DateTime DataFim { get; set; }

        [Required]
        public float TaxaJuros { get; set; }


    }
}
