﻿using System.Net;
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
        public async void VerificarOrcamento(string usuarioId, decimal valorParaInvestir, DateTime dataObjetivo)
        {
            var orcamento = await _budgetRepository.ConsultarOrcamentoPorUsuarioId(usuarioId);
            var meses = CalcularDiferencaEmMeses(orcamento.StartDate, orcamento.EndDate);
            var valorTotal = orcamento.Amount * meses;

            // verificar a data de objetivo, com o salario da pessoa se vai sobrar o dinheiro que a pessoa quer economizar
            if (orcamento == null || valorTotal < valorParaInvestir)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, "Não é possível criar uma meta de investimento com esse orçamento. Tente um valor mais adequado para ajeitar sua vida financeira! :)");
            }
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
