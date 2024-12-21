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
        //ajuste no card #2 https://github.com/users/Rafael-Oliveira-Gomes/projects/3/views/1?pane=issue&itemId=90015029&issue=Rafael-Oliveira-Gomes%7CCoinCompassAPI%7C2
        public async void VerificarOrcamento(int usuarioId, decimal valorParaInvestir, DateTime dataObjetivo)
        {
            //var orcamento = await _budgetRepository.ConsultarOrcamentoPorUsuarioId(usuarioId);
            //var meses = CalcularDiferencaEmMeses(orcamento.StartDate, orcamento.EndDate);
            //var valorTotal = orcamento.Amount * meses;

            //// verificar a data de objetivo, com o salario da pessoa se vai sobrar o dinheiro que a pessoa quer economizar
            //if (orcamento == null || valorTotal < valorParaInvestir)
            //{
            //    throw new HttpResponseException(HttpStatusCode.BadRequest, "Não é possível criar uma meta de investimento com esse orçamento. Tente um valor mais adequado para ajeitar sua vida financeira! :)");
            //}
        }


        public static int CalcularDiferencaEmMeses(DateTime dataInicio, DateTime dataFim)
        {
            int meses = ((dataFim.Year - dataInicio.Year) * 12) + dataFim.Month - dataInicio.Month;
            if (meses == 0)
            {
                meses = 1;
            }
            return meses;
        }
    }
}
