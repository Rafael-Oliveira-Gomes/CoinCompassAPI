using System.ComponentModel.DataAnnotations;
using CoinCompassAPI.Domain.Entities;

namespace CoinCompassAPI.Application.DTOs.Account
{
    public class ReadAccountDto
    {
        public virtual ApplicationUser User { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string TipoConta { get; set; }
        public decimal Saldo { get; set; }
        public string NomeBanco { get; set; }
    }
}
