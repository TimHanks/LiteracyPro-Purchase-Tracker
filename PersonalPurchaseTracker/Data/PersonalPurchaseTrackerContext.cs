using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalPurchaseTracker.Models;

namespace PersonalPurchaseTracker.Data
{
    public class PersonalPurchaseTrackerContext : DbContext
    {
        public PersonalPurchaseTrackerContext (DbContextOptions<PersonalPurchaseTrackerContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
              .Property(p => p.ID)
              .ValueGeneratedOnAdd();
        }


        public DbSet<PersonalPurchaseTracker.Models.Transaction> Transaction { get; set; }


        public DbSet<PersonalPurchaseTracker.Models.Category> Category { get; set; }
    }
}
