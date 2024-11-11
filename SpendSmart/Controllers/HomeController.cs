using Microsoft.AspNetCore.Mvc;
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
