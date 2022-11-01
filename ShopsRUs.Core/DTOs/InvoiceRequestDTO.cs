using ShopsRUs.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopsRUs.Core.DTOs
{
    public class InvoiceRequestDTO
    {
        public string CustomerId { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}
