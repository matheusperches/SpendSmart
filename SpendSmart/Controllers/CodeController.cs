using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpendSmart.Models;

namespace SpendSmart.Controllers
{
    public class CodeController(SpendSmartDbContext context) : Controller
    {
        private readonly SpendSmartDbContext _context = context;

        public IActionResult Index()
        {
            return View();
        }

        // Generate a new random code and saving it to  the database
        [HttpPost]
        public async Task<IActionResult> CreateCode()
        {
            var code = new Code();
            _context.Codes.Add(code);
            await _context.SaveChangesAsync();

            TempData["GeneratedCode"] = code.Value;
            ViewBag.GeneratedCode = code.Value;
            return View("Index");
        }

        // Fetch expenses associated with a code
        public async Task<IActionResult> ViewExpenses(string codeValue)
        {
            var code = await _context.Codes
                .Include(c => c.Expenses)
                .FirstOrDefaultAsync(c => c.Value == codeValue);

            if (code == null)
            {
                ViewBag.Message = "Code not found!";
                return View("Index");
            }

            return View("Expenses", code.Expenses);
        }
    }
}
