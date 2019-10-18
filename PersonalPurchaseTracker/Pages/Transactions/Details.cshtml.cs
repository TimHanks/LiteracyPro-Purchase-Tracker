using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PersonalPurchaseTracker.Data;
using PersonalPurchaseTracker.Models;

namespace PersonalPurchaseTracker.Pages.Transactions
{
    public class DetailsModel : PageModel
    {
        private readonly PersonalPurchaseTracker.Data.PersonalPurchaseTrackerContext _context;

        public DetailsModel(PersonalPurchaseTracker.Data.PersonalPurchaseTrackerContext context)
        {
            _context = context;
        }

        public Transaction Transaction { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Transaction = await _context.Transaction.FirstOrDefaultAsync(m => m.ID == id);

            if (Transaction == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
