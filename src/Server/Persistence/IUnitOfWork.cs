using Blanche.Domain.Customers;
using Blanche.Domain.Formulas;
using Blanche.Domain.Invoices;
using Blanche.Domain.Products;
using Blanche.Domain.Reservations;
using Blanche.Server.Persistence.Repository;

namespace Blanche.Server.Persistence
{
	public interface IUnitOfWork : IDisposable
	{
        BlancheDbContext DbContext { get; }

        IRepository<Customer> Customers { get; }
        IRepository<Formula> Formulas { get; }
        IRepository<Beer> Beers { get; }
        IRepository<Product> Products { get; }
        IRepository<Invoice> Invoices { get; }
        IRepository<Reservation> Reservations { get; }
        IRepository<PopularDate> PopularDates { get; }

        Task CommitAsync();
        void Commit();
	}
}
