using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalPurchaseTracker.Models
{
    /// <summary>
    /// Transaction Class.  This is a combination of a 'domain object' that reflects the database and the 'dto' used by the front end.
    /// Normally I would separate this into separate classes to make it more modular and shareable, but due to time constraints and the
    /// simple nature of the program, this will suffice.
    /// </summary>
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string Payee { get; set; }

        [Required]
        [Display(Name = "Purchase Category")]
        public int CategoryID { get; set; }

        public virtual Category PurchaseCategory { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0, 1000000)]
        [Display(Name = "Purchase Amount")]
        public decimal PurchaseAmount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(70)]
        public string Memo { get; set; }

    }
}
