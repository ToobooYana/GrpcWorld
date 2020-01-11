using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrpcWorld.Entities;
using GrpcWorld.Performance.Server.Grpc.Extensions;
using Server.Repository.Repositories;

namespace GrpcWorld.Performance.Server.Grpc.Services
{
  public interface IInternalCategoryRepository
  {
    Task<IEnumerable<Entities.Category>> FetchAllCategories(int count);
    void Initialize(int maxCount);
  }

  public class InternalCategoryRepository : IInternalCategoryRepository
  {
    private readonly ICategoryRepository _repository;
    private IList<Category> _categories;

    public InternalCategoryRepository(ICategoryRepository repository)
    {
      _repository = repository;
    }

    public void Initialize(int maxCount)
    {
      var items = _repository.FetchAllAsync(maxCount).GetAwaiter().GetResult();

      _categories = items.Select(i => i.MapToMessage()).ToList();

    }

    public async Task<IEnumerable<Entities.Category>> FetchAllCategories(int count)
    {
      return await Task.FromResult(_categories.Take(count));
    }
  }
}
