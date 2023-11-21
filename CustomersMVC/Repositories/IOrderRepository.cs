using CustomersMVC.Data;

namespace CustomersMVC.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersByCustomerId(int customerId);
    }
}
