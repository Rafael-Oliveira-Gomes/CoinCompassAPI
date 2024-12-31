using System.ComponentModel.DataAnnotations;

namespace CoinCompassAPI.Application.DTOs.Transaction
{
    public class ReadTransactionDto
    {
        public string UserName {  get; set; }
        public string Email { get; set; }
        public int ContaId { get; set; }
        public string Tipo { get; set; }
        public decimal Quantia { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
    }
}
