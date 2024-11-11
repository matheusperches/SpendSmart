using System.ComponentModel.DataAnnotations;

namespace SpendSmart.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Value is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value greater than 0.")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }
    }
}
