using System.ComponentModel.DataAnnotations;

namespace CoinCompassAPI.Application.DTOs.Investment
{
    public class ReadInvestmentDto
    {
        public string UswrName { get; set; }
        public string Email { get; set; }
        public string TipoInvestimento { get; set; }
        public decimal Quantia { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public float TaxaJuros { get; set; }
    }
}
