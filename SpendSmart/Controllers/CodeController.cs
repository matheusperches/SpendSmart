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
        public IActionResult CreateCode()
        {
            string generatedCode;
            do
            {
                generatedCode = Code.GenerateShortCode();

            } while (_context.Codes.Any(c => c.ShortCode == generatedCode));
            var code = new Code {ShortCode = generatedCode };
            _context.Codes.Add(code);
            _context.SaveChanges();

            TempData["GeneratedCode"] = code.ShortCode; // Pass the code to the redirected page
            return RedirectToAction("Index");
        }

        // Fetch expenses associated with a code
        public async Task<IActionResult> ViewExpenses(string codeValue)
        {
            var code = await _context.Codes
                .Include(c => c.Expenses)
                .FirstOrDefaultAsync(c => c.ShortCode == codeValue);

            if (code == null)
            {
                ViewBag.Message = "Code not found!";
                return View("Index");
            }

            return View("Expenses", code.Expenses);
        }
    }
}
