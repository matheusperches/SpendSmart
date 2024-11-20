using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("GetExpensesByCode")]
        public ActionResult<IEnumerable<Expense>> GetExpensesByCode(string codeValue)
        {
            // Find the code and the associated expenses 
            var code = _context.Codes.Include(c => c.Expenses).FirstOrDefault(c => c.Value == codeValue);
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

        public IActionResult CreateEditExpenseForm(Expense model)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    // Creating new expense
                    _context.Expenses.Add(model);
                }
                else
                {
                    // Editing existing expense
                    _context.Expenses.Update(model);
                }

                _context.SaveChanges();
                return RedirectToAction("Expenses");
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            return View(model);  // Return to the same page if validation fails
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
