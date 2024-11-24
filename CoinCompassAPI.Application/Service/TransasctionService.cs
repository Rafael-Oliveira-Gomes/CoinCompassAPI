using CoinCompassAPI.Application.DTOs.SavingsGoal;
using CoinCompassAPI.Application.DTOs.Transaction;
using CoinCompassAPI.Application.Interface;
using CoinCompassAPI.Domain.Entities;
using CoinCompassAPI.Infrastructure.Interface;
using CoinCompassAPI.Infrastructure.Repository;

namespace CoinCompassAPI.Application.Service
{
    public class TransasctionService : ITransactionService
    {
        private readonly ITransacaoRepository _transactionRepository;
        public async Task<bool> AtualizarTransaction(int id, CreateTransactionDto TransactionDto)
        {
            var transacao = await _transactionRepository.ConsultarTansacaoPorID(id);
            if (transacao == null)
            {
                throw new Exception("transacao não encontrado para atualizar.");
            }

            transacao.Update(TransactionDto.ContaId, TransactionDto.Tipo, TransactionDto.Quantia, TransactionDto.Data, TransactionDto.Descricao);
            return true;
        }

        public async Task CadastrarTransaction(CreateTransactionDto TransactionDto)
        {
            var transacao = new Transaction(TransactionDto.ContaId, TransactionDto.Tipo, TransactionDto.Quantia, TransactionDto.Data, TransactionDto.Descricao);
            await _transactionRepository.AddAsync(transacao);
        }

        public Task<IEnumerable<CreateTransactionDto>> ConsultarTransaction(int skip = 0, int take = 20)
        {
            throw new NotImplementedException();
        }

        public async Task<CreateTransactionDto> ConsultarTransactionPorID(int id)
        {
            var transacao = await _transactionRepository.ConsultarTansacaoPorID(id);
            if (transacao == null)
            {
                throw new Exception("transacao não encontrado para consultar!");
            }

            return new CreateTransactionDto
            {

                ContaId = transacao.AccountId,
                Tipo = transacao.Type,
                Quantia = transacao.Amount,
                Data = transacao.Date,
                Descricao = transacao.Description

            };
        }

        public async Task<bool> DeletarTransaction(int id)
        {
            var transacao = await _transactionRepository.ConsultarTansacaoPorID(id);
            if (transacao == null)
            {
                throw new Exception("transacao não encontrado para consultar!");
            }

            await _transactionRepository.DeleteAsync(transacao);

            return true;
        }
    }
}
