using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalPurchaseTracker.Models
{
    /// <summary>
    /// This is needed to handle the format sent by the Kendo DataSource
    /// </summary>
    public class KendoTransactionRequest
    {
        public IEnumerable<Transaction> models { get; set; }
    }
}
