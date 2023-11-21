using AutoMapper;
using CustomersMVC.Data;
using CustomersMVC.Models;
using CustomersMVC.Repositories;

namespace CustomersMVC.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<OrderLine>> GetAllOrders(string username)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByUsernameAsync(username);
            
            BaseRepository<Product>? productRepository = _unitOfWork.ProductRepository as BaseRepository<Product>;

            var orders = await _unitOfWork.OrderRepository.GetOrdersByCustomerId(customer!.Id);
            await Console.Out.WriteLineAsync($"Orders: {orders.ToList().Count}");
            
            var products = await productRepository!.GetAllAsync();

            var orderLines = orders.Join(products, order => order.Productid, product => product.Id,
                    (order, product) => new OrderLine
                    {
                        OrderID = order.Orderid,
                        ProductID = product.Id,
                        ProductName = product.Name,
                        ProductQty = product.Quantity,
                        OrderDate = order.Orderdate,
                        Price = product.Price,
                        Cost = product.Price * product.Quantity
                    })
                .ToList();

            return orderLines;
        }
    }
}
