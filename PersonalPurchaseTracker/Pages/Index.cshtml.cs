using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalPurchaseTracker.Models;

namespace PersonalPurchaseTracker.Pages
{
    public class IndexModel : PageModel
    {

        private readonly PersonalPurchaseTracker.Data.PersonalPurchaseTrackerContext _context;

        public IndexModel(PersonalPurchaseTracker.Data.PersonalPurchaseTrackerContext context)
        {
            _context = context;
            CategoryList = _context.Category.OrderBy(a => a.CategoryName).ToList();
            Transactions = _context.Transaction.Include(a => a.PurchaseCategory).ToList();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Transaction Transaction { get; set; }
        [ViewData]
        public List<Category> CategoryList { get; set; }
        public IList<Transaction> Transactions { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                _context.Transaction.Add(Transaction);
                await _context.SaveChangesAsync();
            }
            return Page();
        }


    }
}
