using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Extensions.Logging.Abstractions;
using SpendSmart.Models;
using System.Diagnostics;

namespace SpendSmart.Controllers
{
    public class HomeController(ILogger<HomeController> logger, SpendSmartDbContext context) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;

        private readonly SpendSmartDbContext _context = context;

        public IActionResult Index()
        {
            return View();
        }
        public ActionResult<IEnumerable<Expense>> ExpensesList(string codeValue)
        {
            // Find the code and the associated expenses 
            var code = _context.Codes.Include(c => c.Expenses).First(c => c.Value == codeValue);
            if (code == null)
            {
                TempData["ErrorMessage"] = "Code not found";
                return View("Index");
            }

            return View("ExpensesList", code.Expenses.ToList());
        }
        public IActionResult Expenses() 
        {
            var expenses = _context.Expenses.ToList();
            ViewBag.Expenses = expenses.Sum(e => e.Value); // Calculate total expenses
            return View(expenses);
        }
        public IActionResult CreateExpense(Expense model, string codeValue)
        {
            // Remove Id validation if it's a new expense
            ModelState.Remove("Id");

            // Validating the model 
            if (ModelState.IsValid)
            {
                // Fetch the CodeId from the codeValue (ShortCode) passed in the query string
                var code = _context.Codes.FirstOrDefault(c => c.Value == codeValue);

                if (code == null)
                {
                    // Handle invalid code value
                    ModelState.AddModelError("", "Invalid Code.");
                    return View(model);
                }

                // Set the CodeId in the Expense model
                model.CodeId = code.Id;

                if (model.Id == 0)
                {
                    // Creating a new expense
                    _context.Expenses.Add(model);
                }
                else
                {
                    // Fetching the existing expense, then updating & saving 
                    var existingExpense = _context.Expenses.Find(model.Id);
                    if (existingExpense != null)
                    {
                        existingExpense.Value = model.Value;
                        existingExpense.Description = model.Description;
                        existingExpense.CodeId = model.CodeId;  // Update the CodeId if needed
                    }
                    else
                    {
                        // Handle the case where the expense isn't found
                        ModelState.AddModelError("", "Expense not found.");
                        return View(model);
                    }
                }

                // Save changes to the database
                _context.SaveChanges();

                // Redirect to the expenses list with the codeId
                return RedirectToAction("ExpensesList", new { codeValue });
            }

            TempData["ErrorMessage"] = "Model error.";
            // Return the view with validation errors if any
            return RedirectToAction("ExpensesList");
        }
        [HttpPost]
        public IActionResult UpdateExpense(Expense model, string codeValue)
        {
            var existingExpense = _context.Expenses.SingleOrDefault(e => e.Id == model.Id);
            if (existingExpense == null)
            {
                TempData["ErrorMessage"] = "Expense not found.";
                return RedirectToAction("ExpensesList", new { codeValue });
            }

            // Update expense details
            existingExpense.Value = model.Value;
            existingExpense.Description = model.Description;

            _context.SaveChanges();

            // Redirect to the list to refresh the view
            return RedirectToAction("ExpensesList", new { codeValue });
        }
        public IActionResult DeleteExpense(int id)
        {
            // Find the expense in the database
            var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);

            if (expenseInDb != null)
            {
                // Find the associated code value
                var codeValue = _context.Codes
                    .Where(code => code.Id == expenseInDb.CodeId)
                    .Select(code => code.Value)
                    .FirstOrDefault();

                // Remove the expense
                _context.Expenses.Remove(expenseInDb);
                _context.SaveChanges();

                // Redirect to the ExpensesList with the current codeValue
                return RedirectToAction("ExpensesList", new { codeValue });
            }

            // Handle case where the expense is not found
            TempData["ErrorMessage"] = "Expense not found.";
            return RedirectToAction("ExpensesList");
        }
    }
}
