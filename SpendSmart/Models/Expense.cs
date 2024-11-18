using System.ComponentModel.DataAnnotations;

namespace SpendSmart.Models
{
    public class Code
    { 
        public int Id { get; set; }
        public string Value { get; set; } =  Guid.NewGuid().ToString(); // Generating GUIDs for uniqueness
        public ICollection<Expense> Expenses { get; set; } = [];
    }

    public class Expense
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Value is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value greater than 0.")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }
        public int? CodeId { get; set; } // Foreign Key 
        public Code? Code { get; set; } // Navigation property 
    }
}
