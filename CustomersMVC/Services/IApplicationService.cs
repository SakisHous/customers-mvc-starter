namespace CustomersMVC.Services
{
	public interface IApplicationService
	{
		ICustomerService CustomerService { get; }
		IProductService ProductService { get; }
		IOrderService OrderService { get; }
	}
}
