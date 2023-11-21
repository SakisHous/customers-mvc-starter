using CustomersMVC.Data;
using CustomersMVC.Models;

namespace CustomersMVC.Services
{
    public interface IOrderService
    {
        Task<List<OrderLine>> GetAllOrders(string customerName);
    }
}
