using CoinCompassAPI.Application.DTOs.OutGoigns;

namespace CoinCompassAPI.Application.Interface
{
    public interface IOutgoingsService
    {
        Task CriarOutGoings(CreateOutgoingsDto OutGoingsDto);
        Task<IEnumerable<CreateOutgoingsDto>> ConsultarOutGoings(int skip = 0, int take = 20);
        Task<ReadOutGoingsDto> ConsultarOutGoingsPorID(int id);
        Task<bool> AtualizarOutGoings(int id, CreateOutgoingsDto outGoingstDto);
        Task<bool> DeletarOutGoings(int id);
    }
}
