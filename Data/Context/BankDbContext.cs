using System;
using System.IO;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Context
{
    public class BankDbContext : DbContext
    {
        public static string connectionString;
        public DbSet<BankAccount> Savings;
        public DbSet<Deposit> Deposits;
        public DbSet<Withdrawal> Withdrawals;
        public DbSet<Transfer> Transfers;

        public BankDbContext() : base() { }
        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/appsettings.json")
                .Build();

            connectionString = configuration.GetConnectionString("Default");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder
                .Entity<BankAccount>(entity =>
                {
                    entity.HasKey(account => account.Number);
                    entity
                        .Property(a => a.DateCreated)
                        .ValueGeneratedOnAdd();
                    entity.HasIndex(a => a.Alias).IsUnique();
                    entity
                        .Property(a => a.Type)
                        .IsRequired();
                });

            modelBuilder
                .Entity<Deposit>(entity =>
                {
                    entity.HasKey(d => d.Id);
                    entity
                        .HasOne(d => d.TargetAccount)
                        .WithMany(a => a.Deposits)
                        .HasForeignKey(d => d.TargetAccountId)
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                    entity
                        .Property(a => a.DateCreated)
                        .ValueGeneratedOnAdd();
                });

            modelBuilder
                .Entity<Withdrawal>(entity =>
                {
                    entity.HasKey(w => w.Id);
                    entity
                        .HasOne(w => w.TargetAccount)
                        .WithMany(a => a.Withdrawals)
                        .HasForeignKey(w => w.TargetAccountId)
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                    entity
                        .Property(a => a.DateCreated)
                        .ValueGeneratedOnAdd();
                });

            modelBuilder
                .Entity<Transfer>(entity =>
                {
                    entity.HasKey(t => t.Id);
                    entity
                        .HasOne(t => t.SourceAccount)
                        .WithMany(a => a.TransfersAsSource)
                        .HasForeignKey(t => t.SourceAccountId)
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    entity
                        .HasOne(t => t.TargetAccount)
                        .WithMany(a => a.TransfersAsTarget)
                        .HasForeignKey(t => t.TargetAccountId)
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    entity
                        .Property(a => a.DateCreated)
                        .ValueGeneratedOnAdd();
                });

            SeedData(modelBuilder);
        }

        protected void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>()
                .HasData(
                    new BankAccount(0, Guid.NewGuid()) { DateCreated = DateTime.Now, Alias = "CA.SEED.ALPHA" },
                    new BankAccount(0, Guid.NewGuid()) { DateCreated = DateTime.Now, Alias = "CA.SEED.BETA" },
                    new BankAccount(0, Guid.NewGuid()) { DateCreated = DateTime.Now, Alias = "CA.SEED.GAMMA" },
                    new BankAccount(1, Guid.NewGuid(), 2000) { DateCreated = DateTime.Now, Alias = "CC.SEED.RHO" },
                    new BankAccount(1, Guid.NewGuid(), 1000) { DateCreated = DateTime.Now, Alias = "CC.SEED.EPSILON" },
                    new BankAccount(1, Guid.NewGuid(), 750) { DateCreated = DateTime.Now, Alias = "CC.SEED.OMEGA" }
                ); ;
        }
    }
}
