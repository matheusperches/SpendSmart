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
            var allExpenses = _context.Expenses.ToList();

            var totalExpenses = allExpenses.Sum(x => x.Value);

            ViewBag.Expenses = totalExpenses;

            return View(allExpenses); 
        }
        public IActionResult CreateEditExpense(int? id)
        {
            if (id != null)
            {
                // editing -> load an expense by id 
                var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
                return View();
            }
            return View();
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
