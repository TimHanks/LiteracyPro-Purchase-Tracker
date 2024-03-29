﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PersonalPurchaseTracker.Data;
using PersonalPurchaseTracker.Models;

namespace PersonalPurchaseTracker.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly PersonalPurchaseTracker.Data.PersonalPurchaseTrackerContext _context;

        public IndexModel(PersonalPurchaseTracker.Data.PersonalPurchaseTrackerContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            Category = await _context.Category.ToListAsync();
        }
    }
}
