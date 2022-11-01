using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopsRUs.Domain.Models;

namespace ShopsRUs.Infrastructure
{
    public class ShopsRUsDbContext : IdentityDbContext<Customer>
    {
        public ShopsRUsDbContext(DbContextOptions<ShopsRUsDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }

    }
}
