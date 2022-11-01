using ShopsRUs.Core.Interfaces;
using ShopsRUs.Domain.Models;

namespace ShopsRUs.Infrastructure.Repository
{
    public class DiscountRepository : Repository<Discount>, IDiscountRepository
    {
        public DiscountRepository(ShopsRUsDbContext context) : base(context)
        {
        }
    }
}
