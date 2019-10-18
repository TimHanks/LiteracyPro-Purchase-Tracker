using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalPurchaseTracker.Data;
using PersonalPurchaseTracker.Models;

namespace PersonalPurchaseTracker.Pages.Transactions
{
    public class EditModel : PageModel
    {
        private readonly PersonalPurchaseTracker.Data.PersonalPurchaseTrackerContext _context;

        public EditModel(PersonalPurchaseTracker.Data.PersonalPurchaseTrackerContext context)
        {
            _context = context;
            this.CategoryList = _context.Category.OrderBy(a => a.CategoryName).ToList();
        }

        [BindProperty]
        public Transaction Transaction { get; set; }
        [ViewData]
        public List<Category> CategoryList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Transaction = await _context.Transaction.Include(a => a.PurchaseCategory).FirstOrDefaultAsync(m => m.ID == id);

            if (Transaction == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(Transaction.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TransactionExists(int id)
        {
            return _context.Transaction.Any(e => e.ID == id);
        }
    }
}
