using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using SpendSmart.Models;
using Microsoft.EntityFrameworkCore;

namespace SpendSmart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController(SpendSmartDbContext context) : ControllerBase
    {
        private readonly SpendSmartDbContext _context = context;

        // GET: api/expenses
        [HttpGet]
        public ActionResult<IEnumerable<Expense>> GetExpense() { return _context.Expenses.ToList(); }

        // GET: api/expenses/5
        public ActionResult<Expense> GetExpense(int id)
        {
            var expense = _context.Expenses.Find(id);
            if (expense == null)
            {
                return NotFound();
            }
            return expense;
        }

        // POST: api/expenses
        [HttpPost]
        public ActionResult<Expense> PostExpense(Expense expense)
        {
            _context.Expenses.Add(expense);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
        }

        // PUT: api/expenses/5
        [HttpPut("{id}")]
        public IActionResult PutExpense(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return BadRequest();
            }

            _context.Entry(expense).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Expenses.Any(e => e.Id == id ))
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

        [HttpDelete("{id}")]
        public IActionResult DeleteExpenses(int id)
        {
            var expense = _context.Expenses.Find(id);
            if (expense == null)
            {
                return NotFound();
            }
            _context.Expenses.Remove(expense);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
