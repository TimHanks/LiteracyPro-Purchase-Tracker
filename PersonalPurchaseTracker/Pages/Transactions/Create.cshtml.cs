using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalPurchaseTracker.Data;
using PersonalPurchaseTracker.Models;

namespace PersonalPurchaseTracker.Pages.Transactions
{
    public class CreateModel : PageModel
    {
        private readonly PersonalPurchaseTracker.Data.PersonalPurchaseTrackerContext _context;

        public CreateModel(PersonalPurchaseTracker.Data.PersonalPurchaseTrackerContext context)
        {
            _context = context;
            this.CategoryList = _context.Category.OrderBy(a => a.CategoryName).ToList();

        }

        public IActionResult OnGet()
        {           
            return Page();
        }

        [BindProperty]
        public Transaction Transaction { get; set; }
        [ViewData]
        public List<Category> CategoryList { get; set; }


        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Transaction.Add(Transaction);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
