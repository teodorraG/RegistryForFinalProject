using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistryForFinalProject.Models;

namespace RegistryForFinalProject.Contexts
{
    public class RegistryContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.AccountConnectionString);
            }
        }
    }
}
