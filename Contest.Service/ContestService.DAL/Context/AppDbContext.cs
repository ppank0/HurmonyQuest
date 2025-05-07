using Bogus.DataSets;
using ContestService.DAL.Entities;
using ContestService.DAL.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ContestService.DAL.Context;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Stage> Stages { get; set; }
    public DbSet<Nomination> Nominations { get; set; }
    public DbSet<MusicalInstrument> MusicalInstruments { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Jury> Juries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new TimestampInterceptor());
        optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        DataGenerator.InitBogusData();

        modelBuilder.Entity<Jury>().HasData(DataGenerator.Juries);
        modelBuilder.Entity<Stage>().HasData(DataGenerator.Stages);
        modelBuilder.Entity<Nomination>().HasData(DataGenerator.Nominations);
        modelBuilder.Entity<MusicalInstrument>().HasData(DataGenerator.MusicalInstruments);
        modelBuilder.Entity<Participant>().HasData(DataGenerator.Participants);
    }
}
