using Microsoft.EntityFrameworkCore;

namespace PipeLine.Backend.Context
{
    public class PipeLineInMemoryContext : PipeLineContext
    {
        public PipeLineInMemoryContext(DbContextOptions<PipeLineInMemoryContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }

    }
}
