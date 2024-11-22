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

        [HttpPost]
        public IActionResult CreateEditExpense(List<Expense> expenses)
        {

            foreach (var expense in expenses)
            {
                if (expense.Id == 0)
                {
                    // Add new expense
                    _context.Expenses.Add(expense);
                }
                else
                {
                    // Update existing expense
                    _context.Expenses.Update(expense);
                }
            }
            _context.SaveChanges();

            return RedirectToAction("ExpensesList");  // Redirect to the list of expenses after saving
        }


        public IActionResult Expenses() 
        {
            var expenses = _context.Expenses.ToList();
            ViewBag.Expenses = expenses.Sum(e => e.Value); // Calculate total expenses
            return View(expenses);
        }
        public IActionResult CreateEditExpense(int? id)
        {
            Expense? model;
            if (id.HasValue && id.Value > 0)
            {
                model = _context.Expenses.Find(id.Value);

                if (model == null)
                {
                    return NotFound();
                }
            }
            else
            {
                // Initialize a new Expense instance for creating a new record 
                model = new Expense();
            }
            return View(model);
        }

        public IActionResult DeleteExpense(int id)
        {
            var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
            if (expenseInDb != null)
            {
                _context.Expenses.Remove(expenseInDb);
                _context.SaveChanges();
            }
            return RedirectToAction("Expenses");
        }

        [HttpPost]
        public IActionResult CreateEditExpenseForm(Expense model, string codeValue)
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


        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
