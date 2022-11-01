using ShopsRUs.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Core.Interfaces
{
    public interface IInvoiceService 
    {
        Task<string> GetTotalInvoiceAmount(List<InvoiceItem> items, string customerId);
    }
}
