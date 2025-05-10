using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTracker.API.Models
{
    public class FinanceEntry
    {
        public int Id { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
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
