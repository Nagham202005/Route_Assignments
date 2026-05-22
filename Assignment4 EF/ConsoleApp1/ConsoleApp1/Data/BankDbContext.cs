using Microsoft.EntityFrameworkCore;
using ConsoleApp1.Entities;

namespace ConsoleApp1.Data
{
    public class BankDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=NationalBankDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>().HasKey(b => b.Code);
            modelBuilder.Entity<Account>().HasKey(a => a.AccountNumber);
            modelBuilder.Entity<Transaction>().HasKey(t => t.TransactionNumber);

            modelBuilder.Entity<CustomerAccount>().HasKey(ca => new { ca.CustomerId, ca.AccountNumber });

            modelBuilder.Entity<Branch>()
                .HasOne(b => b.Manager)
                .WithOne(m => m.Branch)
                .HasForeignKey<Branch>(b => b.ManagerId);

            modelBuilder.Entity<Manager>().HasData(
                new Manager { Id = 1, FullName = "Mona Ahmed", Email = "mona@bank.com", PhoneNumber = "011", HireDate = DateTime.Now }
            );
            modelBuilder.Entity<Branch>().HasData(
                new Branch { Code = "CAI-01", Name = "Cairo Main Branch", Address = "Downtown", ManagerId = 1, PhoneNumber = "123" }
            );

            modelBuilder.Entity<Account>().Property(a => a.CurrentBalance).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Transaction>().Property(t => t.Amount).HasColumnType("decimal(18,2)");
        }
    }
}