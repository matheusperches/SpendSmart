using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace SpendSmart.Models
{
    public class Code
    {
        public int Id { get; set; }
        public string ShortCode { get; set; } // User code 
        public ICollection<Expense> Expenses { get; set; } = [];
        public Code()
        {
            ShortCode = GenerateShortCode();
        }
        public static string GenerateShortCode(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var result = new char[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                var byteBuffer = new byte[length];
                rng.GetBytes(byteBuffer);

                for (int i = 0; i < length; i++)
                {
                    result[i] = chars[byteBuffer[i] % chars.Length];
                }
            }
            return new string(result);
        }
    }
        public class Expense
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "Value is required")]
            [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value greater than 0.")]
            public decimal? Value { get; set; }

            [Required(ErrorMessage = "Description is required")]
            public string? Description { get; set; }
            [Required]
            public int CodeId { get; set; } // Foreign Key 
            public Code? Code { get; set; } // Navigation property
        }
}
