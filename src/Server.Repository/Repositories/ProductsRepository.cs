using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenFu;
using GrpcWorld.Models;
using Microsoft.Extensions.Logging;

namespace Server.Repository.Repositories
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> FetchAllAsync(int count);
        Task<Product> GetByIdAsync(long id);
    }

    public class ProductsRepository : IProductsRepository
    {
        private readonly ILogger<ProductsRepository> _logger;
        private bool _initalized = false;

        private IList<Product> _products;

        public ProductsRepository(ILogger<ProductsRepository> logger)
        {
            _logger = logger;
            Initialize();
        }

        private void Initialize()
        {
            if (_initalized) return;

            var categories = A.ListOf<Category>(10);
            var supplier = A.ListOf<Supplier>(10);

            var productId = 1;
            GenFu.GenFu.Configure<Product>()
                .Fill(p => p.ProductId, () => productId++)
                .Fill(p => p.Category).WithRandom(categories)
                .Fill(p => p.Supplier).WithRandom(supplier);

            _products = A.ListOf<Product>(100000);

            _logger.LogInformation("Initializing repository finished.");

            _initalized = true;
        }

        public async Task<IEnumerable<Product>> FetchAllAsync(int count)
        {
            return await Task.FromResult(_products.Take(count).AsEnumerable());
        }

        public async Task<Product> GetByIdAsync(long id)
        {
            return await Task.FromResult(_products.FirstOrDefault(p => p.ProductId == id));
        }
    }
}
