using GTI619_Lab5.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GTI619_Lab5.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public bool NeedNewPassword { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<AuthentificationConfig> AuthentificationConfigs { get; set; }

        public DbSet<LoginConfig> LoginConfigs { get; set; }

        public DbSet<UserLoginLog> UserLoginLogs { get; set; }

        public DbSet<PasswordStore> PasswordStores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AuthentificationConfig>()
            .ToTable("AuthentificationConfig")
            .HasKey(s => s.Id);

            modelBuilder.Entity<LoginConfig>()
            .ToTable("LoginConfig")
            .HasKey(s => s.Id);

            modelBuilder.Entity<UserLoginLog>()
            .ToTable("UserLoginLog")
            .HasKey(s => s.id);

            modelBuilder.Entity<PasswordStore>()
            .ToTable("PasswordStore")
            .HasKey(s => s.id);
        }
    }
}