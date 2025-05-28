
using System.Collections.Generic;
using System;
using System.Linq;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.Shared.Models.Enums;


namespace PipeLine.AdminDesktop.Repos
{
    public class ESkateBoardRepo
    {
        #region Database
        List<ESkateBoard> _eSkateBoards = new()
            {
                new ESkateBoard
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = Guid.NewGuid(),
                    DeviceType = DeviceType.ESkateBoard,
                    Manufacturer = "Boosted",
                    Model = "Boosted Stealth",
                    Name = "Boosted Beast",
                    BatteryCapacity = 19900,
                    MaxChargingSpeed = 2.5,
                    CanBeLocked = false,
                },
                new ESkateBoard
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = Guid.NewGuid(),
                    DeviceType = DeviceType.ESkateBoard,
                    Manufacturer = "Meepo",
                    Model = "Meepo V4 Shuffle",
                    Name = "Meepo Cruiser",
                    BatteryCapacity = 14400,
                    MaxChargingSpeed = 2.0,
                    CanBeLocked = true,
                }
        };
        #endregion

        public List<ESkateBoard> FindAll()
        {
            return _eSkateBoards;
        }

        public void Delete(ESkateBoard eSkateBoard)
        {
            _eSkateBoards.Remove(eSkateBoard);
        }
        public void Save(ESkateBoard eSkateBoard)
        {
            if (eSkateBoard.Id != Guid.Empty)
                Update(eSkateBoard);
            else
                Insert(eSkateBoard);
        }

        private void Insert(ESkateBoard eSkateBoard)
        {
            _eSkateBoards.Add(eSkateBoard);
        }

        private void Update(ESkateBoard eSkateBoard)
        {
            ESkateBoard? eSkateBoardToUpdate = _eSkateBoards.FirstOrDefault(p => p.Id == eSkateBoard.Id);
            //eSkateBoardToUpdate?.Set(eSkateBoard);
        }
    }
}


