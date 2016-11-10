using GTI619_Lab5.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GTI619_Lab5.DAL
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<AuthentificationConfig> AuthentificationConfigs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthentificationConfig>()
            .ToTable("AuthentificationConfig")
            .HasKey(s => s.Id);
        }
    }
}