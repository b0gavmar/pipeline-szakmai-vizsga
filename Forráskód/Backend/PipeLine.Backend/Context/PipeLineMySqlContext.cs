using Microsoft.EntityFrameworkCore;

namespace PipeLine.Backend.Context
{
    public class PipeLineMySqlContext: PipeLineContext
    {
        public PipeLineMySqlContext(DbContextOptions<PipeLineMySqlContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}
