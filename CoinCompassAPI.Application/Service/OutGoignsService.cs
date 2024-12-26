using CoinCompassAPI.Application.DTOs.OutGoigns;
using CoinCompassAPI.Application.Interface;
using CoinCompassAPI.Application.Util;
using CoinCompassAPI.Domain.Entities;
using CoinCompassAPI.Infrastructure.Interface;

namespace CoinCompassAPI.Application.Service
{
    public class OutgoingsService : IOutgoingsService
    {
        private readonly IOutgoingRepository _outgoingsRepository;
        private readonly IUserService _userService;
        private readonly ValidacoesFinanceiras _validacoesFinanceiras;
        public OutgoingsService(IOutgoingRepository outgoingsRepository, IUserService userService, ValidacoesFinanceiras validacoesFinanceiras)
        {
            _validacoesFinanceiras = validacoesFinanceiras;
            _outgoingsRepository = outgoingsRepository;
            _userService = userService;
        }

        public async Task<bool> AtualizarOutGoings(int id, CreateOutgoingsDto outGoingstDto)
        {
            var gasto = await _outgoingsRepository.ConsultarGastosPorID(id);
            if (gasto == null)
            {
                throw new Exception("gasto não encontrado para consultar!");
            }

            gasto.Update(outGoingstDto.TipoDespesa, outGoingstDto.Data, outGoingstDto.Descricao, outGoingstDto.FormaPagamento, outGoingstDto.ValorDespesa);

            return true;
        }

        public Task<IEnumerable<CreateOutgoingsDto>> ConsultarOutGoings(int skip = 0, int take = 20)
        {
            throw new NotImplementedException();
        }

        public async Task<CreateOutgoingsDto> ConsultarOutGoingsPorID(int id)
        {
            var gasto = await _outgoingsRepository.ConsultarGastosPorID(id);
            if (gasto == null)
            {
                throw new Exception("gasto não encontrado para consultar!");
            }

            return new CreateOutgoingsDto
            {
                Descricao = gasto.Description,
                Data = gasto.Date,
                ValorDespesa = gasto.AmountOutGoings,
                FormaPagamento = gasto.TypeOutgoings,
                TipoDespesa = gasto.HowPaid

            };
        }

        public async Task CriarOutGoings(CreateOutgoingsDto OutGoingsDto)
        {
            var currentUser = await _userService.GetCurrentUser();
            _validacoesFinanceiras.VerificarOrcamento(currentUser.Id, OutGoingsDto.ValorDespesa, OutGoingsDto.Data);
            var gasto = new Outgoings(currentUser.Id, OutGoingsDto.TipoDespesa, OutGoingsDto.Data, OutGoingsDto.Descricao, OutGoingsDto.FormaPagamento, OutGoingsDto.ValorDespesa);

            await _outgoingsRepository.AddAsync(gasto);
        }

        public async Task<bool> DeletarOutGoings(int id)
        {
            var gasto = await _outgoingsRepository.ConsultarGastosPorID(id);
            if (gasto == null)
            {
                throw new Exception("gasto não encontrado para consultar!");
            }

            await _outgoingsRepository.DeleteAsync(gasto);

            return true;
        }
    }
}
