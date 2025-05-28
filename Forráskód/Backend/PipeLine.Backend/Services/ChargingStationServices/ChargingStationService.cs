using Microsoft.EntityFrameworkCore;
using PipeLine.Backend.Assemblers.ChargingStationAssemblers;
using PipeLine.Backend.Repos.ChargingStationRepos;
using PipeLine.Backend.Repos.ChargingStationRepos.ChargingPortRepos;
using PipeLine.Backend.Services.Base;
using PipeLine.Shared.Dtos.ChargingStationDto;
using PipeLine.Shared.Models.ChargingStationModels;

namespace PipeLine.Backend.Services.ChargingStationServices
{
    public class ChargingStationService : BaseService<ChargingStation, ChargingStationDto>, IChargingStationService
    {
        private readonly IChargingStationRepo _chargingStationRepo;
        private readonly IChargingStationAssembler _assembler;
        private readonly IChargingPortRepo _chargingPortRepo;

        public ChargingStationService(IChargingStationRepo repo, IChargingStationAssembler assembler, IChargingPortRepo repo2)
            : base(repo, assembler)
        {
            _chargingStationRepo = repo;
            _assembler = assembler;
            _chargingPortRepo = repo2;
        }

        public async Task<int> GetNumberOfChargingStationsAsync()
        {
            return await _chargingStationRepo.GetNumberOfChargingStationsAsync();
        }

        public async Task<IEnumerable<ChargingStationDto>> GetFilteredStationsAsync(string input)
        {
            var stations = await _chargingStationRepo.GetFilteredStationsAsync(input).OrderBy(s => s.Name).ToListAsync();
            return _assembler.ToDtos(stations);
        }

        public async override Task<bool> CanDelete(Guid id)
        {

            var station = await _chargingStationRepo
                                    .GetAllWithNavigation()
                                    .FirstOrDefaultAsync(s => s.Id == id);

            if (station == null)
                return false;

            return station.Ports.All(p => p.IsDisabled);
        }
    }
}
