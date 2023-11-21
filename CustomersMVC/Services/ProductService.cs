using AutoMapper;
using CustomersMVC.Data;
using CustomersMVC.Repositories;

namespace CustomersMVC.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            BaseRepository<Product>? baseRepository =  _unitOfWork.ProductRepository as BaseRepository<Product>;

            return (List<Product>)await baseRepository!.GetAllAsync();
        }
    }
}
