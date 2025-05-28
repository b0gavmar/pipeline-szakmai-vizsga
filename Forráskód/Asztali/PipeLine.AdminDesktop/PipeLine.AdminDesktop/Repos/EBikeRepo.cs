using System.Collections.Generic;
using System;
using System.Linq;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.Shared.Models.Enums;


namespace PipeLine.AdminDesktop.Repos
{
    public class EBikeRepo
    {
        #region Database
        List<EBike> _eBikes = new()
            {
                 new EBike
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = Guid.NewGuid(),
                    DeviceType = DeviceType.EBike,
                    Manufacturer = "ENGWE",
                    Model = "EP-2 Pro",
                    Name = "byd gyár",
                    BatteryCapacity = 13000,
                    MaxChargingSpeed = 2,
                    DetachableBattery = true,
                },
                new EBike
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = Guid.NewGuid(),
                    DeviceType = DeviceType.EBike,
                    Manufacturer = "ENGWE",
                    Model = "Engine Pro",
                    Name = "Városi EBike",
                    BatteryCapacity = 16000,
                    MaxChargingSpeed = 2.5,
                    DetachableBattery = true,
                },
                new EBike
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = Guid.NewGuid(),
                    DeviceType = DeviceType.EBike,
                    Manufacturer = "HIMO",
                    Model = "Z20",
                    Name = "HIMO Kompakt",
                    BatteryCapacity = 10000,
                    MaxChargingSpeed = 2.0,
                    DetachableBattery = false,
                }
            };
        #endregion

        public List<EBike> FindAll()
        {
            return _eBikes;
        }

        public void Delete(EBike eBike)
        {
            _eBikes.Remove(eBike);
        }
        public void Save(EBike eBike)
        {
            if (eBike.Id != Guid.Empty)
                Update(eBike);
            else
                Insert(eBike);
        }

        private void Insert(EBike eBike)
        {
            _eBikes.Add(eBike);
        }

        private void Update(EBike eBike)
        {
            EBike? eBikeToUpdate = _eBikes.FirstOrDefault(p => p.Id == eBike.Id);
            //eBikeToUpdate?.Set(eBike);
        }
    }
}
