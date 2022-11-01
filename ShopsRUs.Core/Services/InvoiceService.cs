using ShopsRUs.Core.Interfaces;
using ShopsRUs.Domain.Enums;
using ShopsRUs.Domain.Models;

namespace ShopsRUs.Core.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public InvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> GetTotalInvoiceAmount(List<InvoiceItem> items, string customerId)
        {
            try
            {
                var customer = await _unitOfWork.CustomerRepository.GetAsync(v => v.Id == customerId);
                if (customer != null)
                {
                    var user = new Customer();
                    decimal affiliateDiscount = (user.CustomerType == CustomerType.Affiliate) ? 0.1m *
                        GetTotalAmountOnBillExcludingGroceries(items) : 0;
                    decimal employeeDiscount = (user.CustomerType == CustomerType.Employee) ? 0.3m *
                        GetTotalAmountOnBillExcludingGroceries(items) : 0;
                    decimal customerDiscount = (DateTime.Now > (user.DateCreated.AddYears(2)) && (user.CustomerType == CustomerType.Customer)) ? 0.05m * GetTotalAmountOnBillExcludingGroceries(items) : 0;
                    var n = ((int)(GetTotalAmountOnBill(items)) / 100);
                    var fiveDolsDiscount = (n < 1) ? 0 : 5 * n;
                    var totalDiscount = (user.CustomerType == CustomerType.Affiliate) ? affiliateDiscount + fiveDolsDiscount
                        : (user.CustomerType == CustomerType.Employee) ? employeeDiscount + fiveDolsDiscount : customerDiscount + fiveDolsDiscount;
                    var AmountOnBillAfterDiscount = GetTotalAmountOnBill(items) - totalDiscount;
                    var invoice = new Invoice
                    {
                        InvoiceAmount = AmountOnBillAfterDiscount,
                        DiscountAmount = totalDiscount,
                        DateCreated = DateTime.Now,
                    };
                    user.Invoices = new List<Invoice>();
                    user.Invoices.Add(invoice);
                    await _unitOfWork.CustomerRepository.Update(user);
                    return AmountOnBillAfterDiscount.ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private static decimal GetTotalAmountOnBill(List<InvoiceItem> items) => items.Select(v => decimal
        .Parse(v.Amount)).ToList().Sum();

        private static decimal GetTotalAmountOnBillExcludingGroceries(List<InvoiceItem> items) => items.Where(v => v.Category != "Groceries").ToList().Select(v => decimal.Parse(v.Amount)).ToList().Sum();

    }
}
