using Microsoft.EntityFrameworkCore.Storage;
using ShopsRUs.Core.Interfaces;

namespace ShopsRUs.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _disposedValue;
        private readonly ShopsRUsDbContext _context;
        private IDbContextTransaction _objTransaction;
        private ICustomerRepository _customerRepo;
        private IInvoiceRepository _invoiceRepo;
        private IDiscountRepository _discountRepository;
        public UnitOfWork(ShopsRUsDbContext context)
        {
            _context = context;
        }
        
        public ICustomerRepository CustomerRepository => _customerRepo ??= new CustomerRepository(_context);
        public IInvoiceRepository InvoiceRepository => _invoiceRepo ??= new InvoiceRepository(_context);
        public IDiscountRepository DiscountRepository => _discountRepository ??= new DiscountRepository(_context);

        public async Task CreateTransaction()
        {
            _objTransaction = await _context.Database.BeginTransactionAsync();
        }
        public async Task Commit()
        {
            await _objTransaction.CommitAsync();
        }
        public async Task Rollback()
        {
            await _objTransaction?.RollbackAsync();
            await _objTransaction.DisposeAsync();
        }
        public async Task Save()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
