
using ExampleApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Services.HotelRoomAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Border> Borders { get; set; }
       
    }
}
