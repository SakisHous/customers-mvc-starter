using CustomersMVC.Data;

namespace CustomersMVC.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
    }
}
