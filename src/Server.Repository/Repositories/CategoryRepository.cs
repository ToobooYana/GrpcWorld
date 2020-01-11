using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenFu;
using GrpcWorld.Models;
using Microsoft.Extensions.Logging;

namespace Server.Repository.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> FetchAllAsync(int count);
        Task<Category> GetByIdAsync(long id);
    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly ILogger<CategoryRepository> _logger;
        private bool _initalized = false;

        private IList<Category> _categories;

        public CategoryRepository(ILogger<CategoryRepository> logger)
        {
            _logger = logger;
            Initialize();
        }

        private void Initialize()
        {
            if (_initalized) return;

            _logger.LogInformation("Initializing category repository....");

            _categories = A.ListOf<Category>(100000);

            _logger.LogInformation("... finished.");

            _initalized = true;
        }

        public async Task<IEnumerable<Category>> FetchAllAsync(int count)
        {
            return await Task.FromResult(_categories.Take(count));
        }

        public async Task<Category> GetByIdAsync(long id)
        {
            return await Task.FromResult(_categories.FirstOrDefault(p => p.Id == id));
        }
    }
}
