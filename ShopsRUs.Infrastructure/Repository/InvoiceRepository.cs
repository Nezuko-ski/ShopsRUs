using ShopsRUs.Core.Interfaces;
using ShopsRUs.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Infrastructure.Repository
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(ShopsRUsDbContext context) : base(context)
        {
        }
    }
}
