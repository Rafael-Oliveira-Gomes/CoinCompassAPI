using System.Net;
using CoinCompassAPI.Infrastructure.Exceptions;
using CoinCompassAPI.Infrastructure.Interface;

namespace CoinCompassAPI.Application.Util
{
    public class ValidacoesFinanceiras
    {
        public readonly IBudgetRepository _budgetRepository;

        public ValidacoesFinanceiras(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }
        public async void VerificarOrcamento(string usuarioId, decimal valor,  DateTime dataObjetivo, DateTime? dataInicio = null, bool investimento = false)
        {
            var orcamento = await _budgetRepository.ConsultarOrcamentoPorUsuarioId(usuarioId);
            decimal saldoConta = orcamento.Amount - valor;
            if (investimento)
            {
                int? mesesInvetimento = CalcularDiferencaEmMeses(dataInicio, dataObjetivo);
                decimal? valorTotalConta = orcamento.Amount * mesesInvetimento;
                decimal? valorTotalInvestimento = valor * mesesInvetimento;

                // verificar a data de objetivo, com o salario da pessoa se vai sobrar o dinheiro que a pessoa quer economizar
                if (orcamento == null || valorTotalConta < valorTotalInvestimento)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest, "Não é possível criar uma meta de investimento com esse orçamento. Tente um valor mais adequado para ajeitar sua vida financeira! :)");
                }
            }

            orcamento.Update(orcamento.Category, saldoConta, orcamento.StartDate, orcamento.EndDate);

        }


        private static int? CalcularDiferencaEmMeses(DateTime? dataInicio, DateTime dataFim)
        {
            int? meses = ((dataFim.Year - dataInicio?.Year) * 12) + dataFim.Month - dataInicio?.Month;
            if (meses == 0)
            {
                meses = 1;
            }
            return meses;
        }
    }
}
