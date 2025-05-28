using Microsoft.EntityFrameworkCore;
using PipeLine.Backend.Assemblers.ChargingStationAssemblers.ChargingPortAssemblers;
using PipeLine.Backend.Context;
using PipeLine.Backend.Repos.ChargingInstanceRepos;
using PipeLine.Backend.Repos.ChargingStationRepos.ChargingPortRepos;
using PipeLine.Backend.Services.Base;
using PipeLine.Shared.Dtos.ChargingStationDtos.ChargingPortDto;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;
using PipeLine.Shared.Responses;

namespace PipeLine.Backend.Services.ChargingStationServices.ChargingPortServices
{
    public class ChargingPortService : BaseService<ChargingPort, ChargingPortDto>, IChargingPortService
    {
        private readonly IChargingPortRepo _chargingPortRepo;
        private readonly IChargingPortAssembler _assembler;
        private readonly IChargingInstanceRepo _chargingInstanceRepo;

        public ChargingPortService(IChargingPortRepo chargingPortRepo, IChargingPortAssembler assembler, IChargingInstanceRepo repo2)
            : base(chargingPortRepo, assembler)
        {
            _chargingPortRepo = chargingPortRepo;
            _assembler = assembler;
            _chargingInstanceRepo = repo2;
        }

        public async Task<int> GetNumberOfPortsAsync()
        {
            return await _chargingPortRepo.GetNumberOfPortsAsync();
        }
        public async Task<int> GetNumberOfInUsePortsAsync()
        {
            return await _chargingPortRepo.GetNumberOfInUsePortsAsync();
        }
        public async Task<int> GetNumberOfEnabledPortsAsync()
        {
            return await _chargingPortRepo.GetNumberOfEnabledPortsAsync();
        }

        public async Task<IEnumerable<ChargingPortDto>> GetFilteredPortsByChargingStationIdAsync(int? input, Guid id)
        {
            var ports = await _chargingPortRepo.GetFilteredPortsByChargingStationIdAsync(input, id);
            return _assembler.ToDtos(ports.ToList());
        }

        public async Task<Response> SetPortToIsBeingUsedByIdAsync(Guid? id)
        {
            try
            {
                await _chargingPortRepo.SetPortToIsBeingUsedByIdAsync(id);
                return new Response(id ?? Guid.Empty);
            }
            catch (Exception ex)
            {
                return new Response($"Error while changing the port's state: {ex.Message}");
            }
        }

        public async Task<Response> SetPortToIsBeingUsedFalseByIdAsync(Guid? id)
        {
            try
            {
                await _chargingPortRepo.SetPortToIsBeingUsedFalseByIdAsync(id);
                return new Response(id ?? Guid.Empty);
            }
            catch (Exception ex)
            {
                return new Response($"Error while changing the port's state: {ex.Message}");
            }
        }

        public async Task<Response> SetPortToIsChargingByIdAsync(Guid? id)
        {
            try
            {
                await _chargingPortRepo.SetPortToIsChargingByIdAsync(id);
                return new Response(id ?? Guid.Empty);
            }
            catch (Exception ex)
            {
                return new Response($"Error while changing the port's state: {ex.Message}");
            }
        }

        public async Task<Response> SetPortToIsChargingFalseByIdAsync(Guid? id)
        {
            try
            {
                await _chargingPortRepo.SetPortToIsChargingFalseByIdAsync(id);
                return new Response(id ?? Guid.Empty);
            }
            catch (Exception ex)
            {
                return new Response($"Error while changing the port's state: {ex.Message}");
            }
        }

        public async Task<int> GetNumberOfPortsOfChargingStationAsync(Guid id)
        {
            return await _chargingPortRepo.GetNumberOfPortsOfChargingStationAsync(id);
        }

        public async override Task<bool> CanDelete(Guid id)
        {
            var port = await _chargingPortRepo.GetByIdAsync(id);
            var hasOngoingInstance = await _chargingInstanceRepo.GetAll()
                    .AnyAsync(ci => ci.ChargingPortId == id && ci.End == null);
            return port.IsDisabled && !hasOngoingInstance;
        }
    }
}
