using System.ComponentModel.DataAnnotations;

namespace CoinCompassAPI.Application.DTOs.Account
{
    public class CreateAccountDto
    {

        [Required]
        public string TipoConta { get; set; }

        [Required]
        public decimal Saldo { get; set; }

        [Required]
        public string NomeBanco { get; set; }

    }
}
