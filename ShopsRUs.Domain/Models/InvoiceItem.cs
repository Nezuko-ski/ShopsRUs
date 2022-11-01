using ShopsRUs.Domain.Interface;

namespace ShopsRUs.Domain.Models
{
    public class InvoiceItem : EntityBase
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Amount { get; set; }

    }
}
