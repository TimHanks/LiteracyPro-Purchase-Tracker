using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PersonalPurchaseTracker.Models
{
    public class Category
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        [DataType(DataType.Text)]
        [StringLength(100)]
        public string CategoryName { get; set; }

    }

    
}
