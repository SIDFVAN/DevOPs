using Blanche.Domain.Customers;
using Blanche.Domain.Formulas;
using Blanche.Domain.Invoices;
using Blanche.Domain.Products;
using Blanche.Domain.Reservations;
using Blanche.Server.Persistence.Repository;

namespace Blanche.Server.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlancheDbContext _dbContext;

        private IRepository<Customer>? _customerRepo;
        private IRepository<Formula>? _formulaRepo;
        private IRepository<Product>? _productRepo;
        private IRepository<Beer>? _beerRepo;
        private IRepository<Invoice>? _invoiceRepo;
        private IRepository<Reservation>? _reservationRepo;
        private IRepository<ReservationItem>? _reservationItemRepo;
        private IRepository<PopularDate>? _popularDateRepo;

        public UnitOfWork(BlancheDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BlancheDbContext DbContext => _dbContext;

        public IRepository<Customer> Customers => _customerRepo ??= (_customerRepo = new Repository<Customer>(_dbContext));
        public IRepository<Formula> Formulas => _formulaRepo ??= (_formulaRepo = new Repository<Formula>(_dbContext));
        public IRepository<Product> Products => _productRepo ??= (_productRepo = new Repository<Product>(_dbContext));
        public IRepository<Beer> Beers => _beerRepo ??= (_beerRepo = new Repository<Beer>(_dbContext));
        public IRepository<Invoice> Invoices => _invoiceRepo ??= (_invoiceRepo = new Repository<Invoice>(_dbContext));
        public IRepository<ReservationItem> ReservationItems => _reservationItemRepo ??= (_reservationItemRepo = new Repository<ReservationItem>(_dbContext));
        public IRepository<Reservation> Reservations => _reservationRepo ??= (_reservationRepo = new Repository<Reservation>(_dbContext));
        public IRepository<PopularDate> PopularDates => _popularDateRepo ??= (_popularDateRepo = new Repository<PopularDate>(_dbContext));

        public Task CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}

