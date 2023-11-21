using CustomersMVC.Data;
using CustomersMVC.DTO;

namespace CustomersMVC.Repositories
{
	public interface ICustomerRepository
	{
		Task<bool> SignUpCustomerAsync(CustomerSignupDTO request);
		Task<Customer?> GetCustomerAsync(string username, string password);
		Task<Customer?> UpdateCustomerAsync(int customerId, CustomerPatchDTO request);
		Task<Customer?> GetByUsernameAsync(string username);
	}
}
