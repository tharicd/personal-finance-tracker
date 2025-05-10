using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTrackerApp.Models
{
    public class FinanceEntry
    {
        public int Id { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required]
        public EntryType Type { get; set; } // Income or Expense
    }

    public enum EntryType
    {
        Income,
        Expense
    }
}