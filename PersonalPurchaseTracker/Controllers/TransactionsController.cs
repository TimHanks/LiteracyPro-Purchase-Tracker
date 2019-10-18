using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PersonalPurchaseTracker.Data;
using PersonalPurchaseTracker.Models;


namespace PersonalPurchaseTracker.Controllers
{
    /// <summary>
    ///  This is a simple API Controller with CRUD actions.  I chose this because we are working with a page that 
    ///  passes data via AJAX and does not need to be integrated with a veiw.  This API should ultimately be separated
    ///  into it's own project so that it may be re-used by other applications that share the same data.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly PersonalPurchaseTrackerContext _context;

        public TransactionsController(PersonalPurchaseTrackerContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransaction()
        {
            return await _context.Transaction.Include(a => a.PurchaseCategory).OrderByDescending(a => a.PurchaseDate).ToListAsync();
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            var transaction = await _context.Transaction.Include(a => a.PurchaseCategory).FirstOrDefaultAsync(i => i.ID == id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutTransaction(int id, Transaction transaction)
        {
            if (id != transaction.ID)
            {
                return BadRequest();
            }
            transaction.CategoryID = transaction.PurchaseCategory.ID;

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        // PUT: api/Transactions/Kendo/5  + Formpost
        // This is required to interface the Kendo DataSource as it submits form data rather that body data in JSON format.
        [HttpPut("/api/Transactions/Kendo/{id}")]
        public async Task<IActionResult> PutTransactionKendo(int id, [FromForm] KendoTransactionRequest transaction)// the ProductsRequest is required in order the list of Product to be correctly bind from the request
        {
            var value = Request.Form["models"];
            var trans = (Transaction)JObject.Parse(value[0]).ToObject(typeof(Transaction));
            return await PutTransaction(id, trans);

        }


        // POST: api/Transactions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            _context.Transaction.Add(transaction);
            await _context.SaveChangesAsync();
            transaction.PurchaseCategory = _context.Category.Find(transaction.CategoryID);
            return CreatedAtAction("GetTransaction", new { id = transaction.ID }, transaction);
        }

        // POST: api/Transactions/Kendo/5  + Formpost
        // This is required to interface the Kendo DataSource as it submits form data rather that body data in JSON format.
        [HttpPost("/api/Transactions/Kendo")]
        public async Task<ActionResult<Transaction>> PostTransactionKendo([FromForm] KendoTransactionRequest transaction)
        {
            var value = Request.Form["models"];
            var trans = (Transaction)JObject.Parse(value[0]).ToObject(typeof(Transaction));
            trans.CategoryID = trans.PurchaseCategory.ID;
            trans.PurchaseCategory = null;
            return await PostTransaction(trans);

        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Transaction>> DeleteTransaction(int id)
        {
            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transaction.Remove(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        private bool TransactionExists(int id)
        {
            return _context.Transaction.Any(e => e.ID == id);
        }
    }
}
