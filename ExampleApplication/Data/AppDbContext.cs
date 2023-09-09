﻿
using ExampleApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleApplication.Data
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