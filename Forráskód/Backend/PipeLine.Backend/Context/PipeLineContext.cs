using PipeLine.Shared.Models.ChargingInstanceModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PipeLine.Shared.Models.ChargingStationModels;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.Shared.Models.ErrorTicketModels;
using PipeLine.Shared.Models.UserModels;


namespace PipeLine.Backend.Context
{
    public class PipeLineContext : IdentityDbContext<ApplicationUser,IdentityRole<Guid>,Guid>
    {


        public DbSet<ChargingStation> ChargingStations { get; set; }
        public DbSet<ChargingInstance> ChargingInstances { get; set; }

        public DbSet<ChargingPort> ChargingPorts { get; set; }

        public DbSet<ErrorTicket> ErrorTickets { get; set; }
        public DbSet<Device> Devices { get; set; }


        public PipeLineContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
