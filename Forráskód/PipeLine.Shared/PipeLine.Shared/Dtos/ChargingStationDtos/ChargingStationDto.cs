﻿using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.Shared.Dtos.ChargingStationDto
{
    public class ChargingStationDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
