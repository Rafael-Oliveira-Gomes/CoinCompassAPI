using Microsoft.EntityFrameworkCore;
using CoinCompassAPI.Domain.Entities;
using CoinCompassAPI.Infrastructure.Interface;
using CoinCompassAPI.Infrastructure.Persistence;

namespace CoinCompassAPI.Infrastructure.Repository
{
    public class BudgetRepository : BaseRepository<Budget>, IBudgetRepository
    {
        public BudgetRepository(DataContext context) : base(context) { }

        public async Task<Budget> ConsultarOrcamentoPorID(int id)
        {
            return await _context.Orcamentos.FindAsync(id);
        }

        public async Task<Budget> ConsultarOrcamentoPorUsuarioId(int usuarioId)
        {
            try
            {
                return await _context.Orcamentos
                                     .FirstOrDefaultAsync(b => b.UserId == usuarioId);
            }
            catch (Exception ex)
            {
                // Registrar o erro para análise
                Console.WriteLine($"Erro ao consultar orçamento: {ex.Message}");
                throw;
            }
        }


        public async Task<IEnumerable<Budget>> ConsultarOrcamentos(int skip, int take)
        {
            return await _context.Orcamentos.Skip(skip).Take(take).ToListAsync();
        }
    }
}
