namespace CoinCompassAPI.Application.DTOs.OutGoigns
{
    public class ReadOutGoingsDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string TipoDespesa { get; set; }

        /// <summary>
        /// Data da Despesa. Requerido.
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        /// Descrição da Despesa. Requerido.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Forma de Pagamento. Requerido.
        /// </summary>
        public string FormaPagamento { get; set; }

        /// <summary>
        /// Valor da Despesa. Requerido.
        /// </summary>
        public decimal ValorDespesa { get; set; }
    }
}
