using ApplicationService.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationService.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) => Database.Migrate();
        public DbSet<ApplicationEntity> Applications { get; set; }
        public DbSet<VideoEntity> Videos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationEntity>()
                .HasOne(a => a.Video)
                .WithOne(v => v.Application)
                .HasForeignKey<ApplicationEntity>(a => a.VideoId);
        }
    }
}

