using INK_API.Models;
using Microsoft.EntityFrameworkCore;

namespace INK_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Ink> Inks { get; set; }
        public DbSet<SchedulePart> ScheduleParts { get; set; }
        public DbSet<SchedulesUpdate> SchedulesUpdates { get; set; }
        public DbSet<TreatmentWay> TreatmentWays { get; set; }
        public DbSet<InkTblObject> InkTblObjects { get; set; }
        public DbSet<Chemical> Chemicals { get; set; }
        public DbSet<Schedules> Schedules { get; set; }

        public DbSet<Glues> Glueses { get; set; }
        public DbSet<PartInkChemical> PartInkChemicals { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<BuildingUser> BuildingUser { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Process> Processes { get; set; }

        public DbSet<Part> Parts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }
        public DbSet<BuildingGlue> BuildingGlues { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasKey(x => x.ID);
        }

    }
}