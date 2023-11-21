using CustomersMVC.Data;
using CustomersMVC.DTO;

namespace CustomersMVC.Services
{
	public interface ICustomerService
	{
		Task SignupCustomerAsync(CustomerSignupDTO request);
		Task<Customer?> LoginCustomerAsync(CustomerLoginDTO credentials);
		Task<Customer> UpdateCustomerAccountInfoAsync(CustomerPatchDTO request, int customerId);
		Task<Customer?> GetCustomerByUsername(string username);
	}
}
