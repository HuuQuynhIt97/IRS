using INK_API.Models;
using Microsoft.EntityFrameworkCore;

namespace INK_API.Data
{
    public class IoTContext : DbContext
    {
        public IoTContext(DbContextOptions<IoTContext> options) : base(options) { }
        public DbSet<RawData> RawData { get; set; }
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RawData>().HasKey(x => x.ID);// um
        }

    }
}