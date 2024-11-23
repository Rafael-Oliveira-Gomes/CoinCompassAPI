using CoinCompassAPI.Application.DTOs.Transaction;

namespace CoinCompassAPI.Application.Interface
{
    public interface ITransactionService
    {
        Task CadastrarTransaction(CreateTransactionDto TransactionDto);
        Task<IEnumerable<CreateTransactionDto>> ConsultarTransaction(int skip = 0, int take = 20);
        Task<CreateTransactionDto> ConsultarTransactionPorID(int id);
        Task<bool> AtualizarTransaction(int id, CreateTransactionDto TransactionDto);
        Task<bool> DeletarTransaction(int id);
    }
}
