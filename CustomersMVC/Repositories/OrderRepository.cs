using CustomersMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomersMVC.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(CustomersDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerId(int customerId)
        {
            return await _context.Orders.Where(x => x.Customerid == customerId).ToListAsync();
        }
    }
}
