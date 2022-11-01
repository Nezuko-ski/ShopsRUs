using ShopsRUs.Domain.Interface;
using System.ComponentModel.DataAnnotations;

namespace ShopsRUs.Domain.Models
{
    public class Invoice : EntityBase
    {
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
