using Microsoft.EntityFrameworkCore;
using MyApp.DataAccess.Abstractions.MyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.DataAccess.Databases.MyDomain
{
    public class MyDomainDbContext : DbContext
    {
        public MyDomainDbContext(DbContextOptions<MyDomainDbContext> options) : base(options)
        {
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Border> Borders { get; set; }

    }
}
