using CustomersMVC.Data;
using CustomersMVC.DTO;
using CustomersMVC.Security;
using Microsoft.EntityFrameworkCore;

namespace CustomersMVC.Repositories
{
	public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
	{
		public CustomerRepository(CustomersDbContext context) : base(context)
		{
		}

		public async Task<bool> SignUpCustomerAsync(CustomerSignupDTO request)
		{
			var existingCustomer = await _context.Customers.FirstOrDefaultAsync(x => x.Username == request.Username);
			if (existingCustomer != null)
			{
				return false;
			}

			var customer = new Customer
			{
				Username = request.Username,
				Password = EncryptionUtil.Encrypt(request.Password!),
				Firstname = request.Firstname,
				Lastname = request.Lastname,
				Email = request.Email
			};

			await _context.Customers.AddAsync(customer);
			return true;
		}

		public async Task<Customer?> GetCustomerAsync(string username, string password)
		{
			var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Username == username);
			if (customer is null)
			{
				return null;
			}

			if (!EncryptionUtil.IsValidPassword(password, customer.Password))
			{
				return null;
			}

			return customer;
		}

		public async Task<Customer?> UpdateCustomerAsync(int customerId, CustomerPatchDTO request)
		{
			var customer = await _context.Customers.Where(x => x.Id == customerId).FirstAsync();

			if (customer is null)
			{
				return null;
			}

			customer.Username = request.Username;
			customer.Password = EncryptionUtil.Encrypt(request.Password!);
			customer.Firstname = request.Firstname;
			customer.Lastname = request.Lastname;

			_context.Customers.Update(customer);
			return customer;
		}

		public async Task<Customer?> GetByUsernameAsync(string username)
		{
			return await _context.Customers.Where(x => x.Username == username).FirstOrDefaultAsync();
		}

	}
}
