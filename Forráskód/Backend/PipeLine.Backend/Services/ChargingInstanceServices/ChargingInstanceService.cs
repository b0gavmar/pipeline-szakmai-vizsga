using Microsoft.EntityFrameworkCore;
using PipeLine.Backend.Assemblers.ChargingInstanceAssemblers;
using PipeLine.Backend.Repos.ChargingInstanceRepos;
using PipeLine.Backend.Repos.ChargingStationRepos.ChargingPortRepos;
using PipeLine.Backend.Repos.DeviceRepos;
using PipeLine.Backend.Services.Base;
using PipeLine.Shared.Dtos.ChargingInstance;
using PipeLine.Shared.Models.ChargingInstanceModels;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;
using PipeLine.Shared.Responses;

namespace PipeLine.Backend.Services.ChargingInstanceServices
{
    public class ChargingInstanceService : BaseService<ChargingInstance, ChargingInstanceDto>, IChargingInstanceService
    {
        private readonly IChargingInstanceRepo _chargingInstanceRepo;
        private readonly IChargingInstanceAssembler _assembler;
        private readonly IChargingPortRepo _chargingPortRepo;
        private readonly IDeviceRepo _deviceRepo;

        public ChargingInstanceService(
            IChargingInstanceRepo chargingInstanceRepo,
            IChargingPortRepo chargingPortRepo,
            IDeviceRepo deviceRepo,
            IChargingInstanceAssembler assembler)
        : base(chargingInstanceRepo, assembler)
        {
            _chargingInstanceRepo = chargingInstanceRepo;
            _chargingPortRepo = chargingPortRepo;
            _deviceRepo = deviceRepo;
            _assembler = assembler;
        }


        public async Task<ChargingProgressResponse> UpdateChargingProgressAsync(Guid instanceId)
        {
            ChargingProgressResponse response = new();
            try
            {
                var chargingInstance = await _chargingInstanceRepo.GetByIdAsync(instanceId);
                if (chargingInstance == null)
                {
                    response.ClearAndAddError("The data couldn't be changed!");
                    return response;
                }

                if (chargingInstance.End != null)
                {
                    response.IsFinished = true;
                    return response;
                }


                if (chargingInstance.EndPercentage > 100)
                {
                    chargingInstance.EndPercentage = 100;
                    await _chargingPortRepo.SetPortToIsChargingFalseByIdAsync(chargingInstance.ChargingPortId);
                    response.IsFinished = true;
                }

                if (chargingInstance.StartingPercentage != null && chargingInstance.End == null && chargingInstance.DeviceId != null)
                {
                    var device = await _deviceRepo.GetByIdAsync(chargingInstance.DeviceId.Value);
                    var chargingPort = await _chargingPortRepo.GetByIdAsync((Guid)chargingInstance.ChargingPortId);

                    if (device == null || chargingPort == null)
                    {
                        response.ClearAndAddError("Device or charging port not found.");
                        response.IsFinished = true;
                        return response;
                    }

                    if (device.BatteryCapacity != null && device.BatteryVoltage != null && device.MaxChargingSpeed != null)
                    {
                        var ts = (TimeSpan)(DateTime.UtcNow - chargingInstance.Start);
                        double chargingTime = ts.TotalHours;
                        double actualChargingSpeed = Math.Min((double)device.MaxChargingSpeed, chargingPort.MaxChargingSpeed);
                        double energyCharged = actualChargingSpeed * chargingTime;
                        double batteryCapacitykWh = (double)(device.BatteryCapacity * device.BatteryVoltage) / 1_000_000.0;
                        double chargePercentage = energyCharged / batteryCapacitykWh * 100;

                        double currentPercentage = (double)(chargingInstance.StartingPercentage + chargePercentage);

                        if (currentPercentage >= chargingInstance.DesiredEndPercentage && chargingInstance.DesiredEndPercentage != null)
                        {
                            chargingInstance.EndPercentage = chargingInstance.DesiredEndPercentage;
                            await _chargingPortRepo.SetPortToIsChargingFalseByIdAsync(chargingInstance.ChargingPortId);
                            response.IsFinished = true;
                        }
                        else
                        {
                            chargingInstance.EndPercentage = (int?)currentPercentage;
                        }

                        var updateResult = await _chargingInstanceRepo.UpdateAsync(chargingInstance);
                        Response.CombineWith(response, updateResult);
                    }
                }
            }
            catch(Exception e)
            {
                response.ClearAndAddError($"Error while updating charginginstance: {e.Message}");
            }
           
            return response;
        }

        public async Task<Response> FinishChargingInstanceAsync(ChargingInstanceDto dto)
        {
            try
            {
                var entity = await _chargingInstanceRepo.GetByIdAsync(dto.Id);

                if (entity == null)
                {
                    return Response.Failure("Charging instance not found.");
                }
                entity.End = DateTime.UtcNow;

                await _chargingPortRepo.SetPortToIsChargingFalseByIdAsync(entity.ChargingPortId);
                await _chargingPortRepo.SetPortToIsBeingUsedFalseByIdAsync(entity.ChargingPortId);
                return await _chargingInstanceRepo.FinishInstance(entity);
            }
            catch(Exception e)
            {
                return Response.Failure($"Error while ending charginginstance: {e.Message}");
            }
           
        }

        public async Task<Response> StartChargingInstanceAsync(ChargingInstanceDto dto)
        {
            try
            {
                var entity = _assembler.ToModel(dto);
                entity.Start = DateTime.UtcNow;
                var device = await _deviceRepo.GetByIdAsync(entity.DeviceId.Value);
                if (device.BatteryCapacity == null || device.BatteryCapacity == 0 || device.BatteryVoltage == null || device.BatteryVoltage == 0 || device.MaxChargingSpeed == null || device.MaxChargingSpeed == 0)
                {

                }
                else
                {
                    if (entity.StartingPercentage == null)
                    {
                        entity.StartingPercentage = 0;
                    }
                    if (entity.DesiredEndPercentage == null)
                    {
                        entity.DesiredEndPercentage = 100;
                    }
                }
                    
                await _chargingPortRepo.SetPortToIsChargingByIdAsync(entity.ChargingPortId);
                await _chargingPortRepo.SetPortToIsBeingUsedByIdAsync(entity.ChargingPortId);
                return await _chargingInstanceRepo.CreateAsync(entity);
            }
            catch(Exception e)
            {
                return Response.Failure($"Error while starting a charginginstance: {e.Message}");
            }
        }

        public async Task<IEnumerable<ChargingInstanceDto>> GetFilteredInstancesByUserIdAsync(Guid userId, DateTime? start, DateTime? end, bool? state = null)
        {
            var instances = await _chargingInstanceRepo.GetFilteredInstancesByUserIdAsync(start, end, userId, state).ToListAsync();
            return _assembler.ToDtos(instances.ToList());
        }

        public async Task<int> GetNumberOfInstancesByUserIdAsync(Guid userId)
        {
            return await _chargingInstanceRepo.GetNumberOfInstancesByUserIdAsync(userId);
        }

        public async Task<int> GetNumberOfInstancesAsync()
        {
            return await _chargingInstanceRepo.GetNumberOfInstancesAsync();
        }

        public async Task<ChargingInstance> GetInstanceByIdAsync(Guid id)
        {
            return await _chargingInstanceRepo.GetInstanceByIdAsync(id);
        }
    }

}
