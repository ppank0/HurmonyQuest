using ContestService.DAL.Entities;
using ContestService.DAL.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace ContestService.DAL.Context;

internal class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Stage> Stages { get; set; }
    public DbSet<Nomination> Nominations { get; set; }
    public DbSet<MusicalInstrument> MusicalInstruments { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Jury> Juries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new TimestampInterceptor());
    }
}
