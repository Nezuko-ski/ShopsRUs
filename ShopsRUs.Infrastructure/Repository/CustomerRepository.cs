using Microsoft.EntityFrameworkCore;
using ShopsRUs.Core.Interfaces;
using ShopsRUs.Domain.Models;

namespace ShopsRUs.Infrastructure.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly ShopsRUsDbContext _context;
        public CustomerRepository(ShopsRUsDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }
    }  
}
