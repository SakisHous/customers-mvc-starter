using CustomersMVC.Data;

namespace CustomersMVC.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly CustomersDbContext _context;

		public UnitOfWork(CustomersDbContext context)
		{
			_context = context;
		}

		public ICustomerRepository CustomerRepository => new CustomerRepository(_context);
        public IProductRepository ProductRepository => new ProductRepository(_context);
		public IOrderRepository OrderRepository => new OrderRepository(_context);

        public async Task<bool> SaveAsync()
		{
			return await _context.SaveChangesAsync() > 0;
		}
	}
}
