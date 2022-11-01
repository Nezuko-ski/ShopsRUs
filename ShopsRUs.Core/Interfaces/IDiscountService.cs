using ShopsRUs.Domain.Models;

namespace ShopsRUs.Core.Interfaces
{
    public interface IDiscountService
    {
        Task<bool> CreateDiscount(Discount discount);
        Task<List<Discount>> GetAllDiscountsAsync();
        Task<Discount> GetSpecificDiscount(string Id);
    }
}
