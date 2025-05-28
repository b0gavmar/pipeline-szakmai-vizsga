//using PipeLine.Shared.Models.ChargingStationModels;
//using PipeLine.Shared.Models.DeviceModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PipeLine.AdminDesktop.Repos
//{
//    public class ChargingStationRepo
//    {
//        //#region Database
//        //List<ChargingStation> _chargingStations = new List<ChargingStation>
//        //{
//        //        new ChargingStation
//        //        {
//        //            Id = Guid.NewGuid(),
//        //            Name = "Downtown Charging Station",
//        //            Latitude = 37.7749,
//        //            Longitude = -122.4194,
//        //            Address = "123 Main Street, San Francisco, CA"
//        //        },
//        //        new ChargingStation
//        //        {
//        //            Id = Guid.NewGuid(),
//        //            Name = "City Center Fast Charger",
//        //            Latitude = 34.0522,
//        //            Longitude = -118.2437,
//        //            Address = "456 Elm Street, Los Angeles, CA"
//        //        },
//        //        new ChargingStation
//        //        {
//        //            Id = Guid.NewGuid(),
//        //            Name = "Green Energy Hub",
//        //            Latitude = 40.7128,
//        //            Longitude = -74.0060,
//        //            Address = "789 Broadway, New York, NY"
//        //        },
//        //        new ChargingStation
//        //        {
//        //            Id = Guid.NewGuid(),
//        //            Name = "Suburban EV Stop",
//        //            Latitude = 41.8781,
//        //            Longitude = -87.6298,
//        //            Address = "101 Pine Street, Chicago, IL"
//        //        },
//        //        new ChargingStation
//        //        {
//        //            Id = Guid.NewGuid(),
//        //            Name = "Highway Rapid Charger",
//        //            Latitude = 29.7604,
//        //            Longitude = -95.3698,
//        //            Address = "202 Highway Ave, Houston, TX"
//        //        }
//        //    };
//        //#endregion

//        public List<ChargingStation> FindAll()
//        {
//            return _chargingStations;
//        }

//        public void Delete(ChargingStation chargingStation)
//        {
//            _chargingStations.Remove(chargingStation);
//        }
//        public void Save(ChargingStation chargingStation)
//        {
//            if (chargingStation.Id != Guid.Empty)
//                Update(chargingStation);
//            else
//                Insert(chargingStation);
//        }

//        private void Insert(ChargingStation chargingStation)
//        {
//            _chargingStations.Add(chargingStation);
//        }

//        private void Update(ChargingStation chargingStation)
//        {
//            ChargingStation? chargingStationToUpdate = _chargingStations.FirstOrDefault(p => p.Id == chargingStation.Id);
//            //chargingStationToUpdate?.Set(chargingStation);
//        }
//    }
//}
