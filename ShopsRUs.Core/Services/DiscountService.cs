using Microsoft.EntityFrameworkCore;
using ShopsRUs.Core.Interfaces;
using ShopsRUs.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Core.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DiscountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateDiscount(Discount discount)
        {
            try
            {
                 await _unitOfWork.DiscountRepository.InsertAsync(discount);
                 await _unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Discount>> GetAllDiscountsAsync()
        {
            try
            {
              return await _unitOfWork.DiscountRepository.GetAllAsync().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Discount> GetSpecificDiscount(string Id)
        {
            try
            {
               return await _unitOfWork.DiscountRepository.GetAsync(v => v.Id.ToString() == Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
