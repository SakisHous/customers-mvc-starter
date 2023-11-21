using AutoMapper;
using CustomersMVC.Data;
using CustomersMVC.DTO;
using CustomersMVC.Repositories;

namespace CustomersMVC.Services
{
	public class CustomerService : ICustomerService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task SignupCustomerAsync(CustomerSignupDTO request)
		{
			if (!await _unitOfWork.CustomerRepository.SignUpCustomerAsync(request))
			{
				throw new ApplicationException("User already exists");
			}

			await _unitOfWork.SaveAsync();
		}

		public async Task<Customer?> LoginCustomerAsync(CustomerLoginDTO credentials)
		{
			var customer = await _unitOfWork.CustomerRepository.GetCustomerAsync(credentials.Username!, credentials.Password!);

			if (customer is null)
			{
				return null;
			}

			return customer;
		}

		public async Task<Customer> UpdateCustomerAccountInfoAsync(CustomerPatchDTO request, int customerId)
		{
			var customer = await _unitOfWork.CustomerRepository.UpdateCustomerAsync(customerId, request);
			await _unitOfWork.SaveAsync();

			return customer!;
		}

		public async Task<Customer?> GetCustomerByUsername(string username)
		{
			return await _unitOfWork.CustomerRepository.GetByUsernameAsync(username);
		}

	}
}
