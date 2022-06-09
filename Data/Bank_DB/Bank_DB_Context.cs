using Bank_DB_MVC_Redis_DC.Models.Bank_DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank_DB_MVC_Redis_DC.Data.Bank_DB
{
    public class Bank_DB_Context : DbContext
    {
        public static int instanceCounter = 0;

        public DbSet<Bank_Tabelle> Bank_Tabelle { get; set; }

        public Bank_DB_Context() { Database.EnsureCreated(); instanceCounter++; }

        public Bank_DB_Context(DbContextOptions<Bank_DB_Context> options) : base(options) { Database.EnsureCreated(); instanceCounter++; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Bank_DB");
            }
        }

        // using Microsoft.EntityFramework & Data\Bank_DB\Bank_Tabelle-w-PK.json
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Bank_Tabelle>().Property(bank_tabelle => bank_tabelle.Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        //    base.OnModelCreating(modelBuilder);
        //}

        // using Microsoft.EntityFrameworkCore & Data\Bank_DB\Bank_Tabelle-w-PK.json
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Bank_Tabelle>().Property(bank_tabelle => bank_tabelle.Key).ValueGeneratedNever();
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
