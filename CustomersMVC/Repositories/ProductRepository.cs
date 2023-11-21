using CustomersMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomersMVC.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(CustomersDbContext context) : base(context)
        {
        }

    }
}
