using AutoMapper;
using CustomersMVC.Repositories;

namespace CustomersMVC.Services
{
	public class ApplicationService : IApplicationService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public ApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public ICustomerService CustomerService => new CustomerService(_unitOfWork, _mapper);
		public IProductService ProductService => new ProductService(_unitOfWork);
		public IOrderService OrderService => new OrderService(_unitOfWork);
		
	}
}
