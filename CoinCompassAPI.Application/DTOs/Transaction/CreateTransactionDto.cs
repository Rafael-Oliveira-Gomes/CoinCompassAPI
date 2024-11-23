using System.ComponentModel.DataAnnotations;

namespace CoinCompassAPI.Application.DTOs.Transaction
{
    public class CreateTransactionDto
    {
        [Required]
        public int ContaId { get; set; }

        [Required]
        public string Tipo { get; set; }

        [Required]
        public decimal Quantia { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public string Descricao { get; set; }

    }
}
