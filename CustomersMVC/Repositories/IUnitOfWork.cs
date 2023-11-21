namespace CustomersMVC.Repositories
{
	public interface IUnitOfWork
	{
		public ICustomerRepository CustomerRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IOrderRepository OrderRepository { get; }


        Task<bool> SaveAsync();
	}
}
