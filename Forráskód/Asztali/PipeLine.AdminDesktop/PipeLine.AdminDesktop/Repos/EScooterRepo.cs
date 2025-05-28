
using System.Collections.Generic;
using System;
using System.Linq;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.Shared.Models.Enums;


namespace PipeLine.AdminDesktop.Repos
{
    public class EScooterRepo
    {
        #region Database
        List<EScooter> _eScooters = new()
            {
                new EScooter
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = Guid.NewGuid(),
                    DeviceType = DeviceType.EScooter,
                    Manufacturer = "Xiaomi",
                    Model = "Mi Electric Scooter Pro 2",
                    Name = "Xiaomi Pro",
                    BatteryCapacity = 12400,
                    MaxChargingSpeed = 2.1,
                    IsFoldable = true,
                },
                new EScooter
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = Guid.NewGuid(),
                    DeviceType = DeviceType.EScooter,
                    Manufacturer = "Segway",
                    Model = "Ninebot MAX G30",
                    Name = "Segway Max",
                    BatteryCapacity = 15300,
                    MaxChargingSpeed = 3.0,
                    IsFoldable = false,
                }
        };
        #endregion

        public List<EScooter> FindAll()
        {
            return _eScooters;
        }

        public void Delete(EScooter eScooter)
        {
            _eScooters.Remove(eScooter);
        }
        public void Save(EScooter eScooter)
        {
            if (eScooter.Id != Guid.Empty)
                Update(eScooter);
            else
                Insert(eScooter);
        }

        private void Insert(EScooter eScooter)
        {
            _eScooters.Add(eScooter);
        }

        private void Update(EScooter eScooter)
        {
            EScooter? eScooterToUpdate = _eScooters.FirstOrDefault(p => p.Id == eScooter.Id);
            //eScooterToUpdate?.Set(eScooter);
        }
    }
}