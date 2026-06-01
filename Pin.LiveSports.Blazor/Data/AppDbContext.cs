using Microsoft.EntityFrameworkCore;
using Pin.LiveSports.Core.Models;

namespace Pin.LiveSports.Blazor.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Country> Countries { get; set; }
    public DbSet<Player> Players { get; set; }

}