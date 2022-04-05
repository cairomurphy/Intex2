using System;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationLab.Models
{
    public class CrashDbContext : DbContext
    {
        public CrashDbContext(DbContextOptions<CrashDbContext> options) : base(options)
        {

        }

        public DbSet<Crash> mytable { get; set; }
        public DbSet<Severity> Severities { get; set; }
    }
}