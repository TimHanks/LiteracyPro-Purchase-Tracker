using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonalPurchaseTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalPurchaseTracker.Models
{
    public class SeedData
    {
        /// <summary>
        /// This will seed the database with some categories and transactions
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PersonalPurchaseTrackerContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<PersonalPurchaseTrackerContext>>()))
            {
                if (context.Category.Any())
                {
                    return;
                }

                context.Category.AddRange(
                    new Category
                    {
                       CategoryName = "Train"
                    },
                   new Category
                   {
                       CategoryName = "Per Diem"
                   },
                   new Category
                   {
                       CategoryName = "Meals"
                   },
                   new Category
                   {
                       CategoryName = "Air Travel"
                   },
                   new Category
                   {
                       CategoryName = "Software Purchase"
                   }
                );
                if (context.Transaction.Any())
                {
                    return;   // DB has been seeded
                }

                context.Transaction.AddRange(
                    new Transaction
                    {
                        CategoryID = 1,
                        Payee = "Boulder Express",
                        PurchaseAmount = 56.76M,
                        Memo = "Train To Boulder",
                        PurchaseDate = DateTime.Now.AddDays(-3)
                    },
                    new Transaction
                    {
                        CategoryID = 3,
                        Payee = "Huckleberry",
                        PurchaseAmount = 105.08M,
                        Memo = "Dinner at Huckleberry",
                        PurchaseDate = DateTime.Now.AddDays(-2)
                    },
                    new Transaction
                    {
                        CategoryID = 4,
                        Payee = "Madeup Airlines",
                        PurchaseAmount = 105.08M,
                        Memo = "Flight Back to Denver",
                        PurchaseDate = DateTime.Now
                    },
                     new Transaction
                     {
                         CategoryID = 5,
                         Payee = "Tim Hanks",
                         PurchaseAmount = 200.00M,
                         Memo = "Creation of Purchase Tracker",
                         PurchaseDate = DateTime.Now.AddDays(-1)
                     }
                );
                context.SaveChanges();
            }
        }
    }
}
