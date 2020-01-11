using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcWorld.Basic.Server.Extensions;
using Microsoft.Extensions.Logging;
using Server.Repository.Repositories;
using Services;

namespace GrpcWorld.Basic.Server.Services
{
  public class CategoryService : Categories.CategoriesBase
  {
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger)
    {
      _categoryRepository = categoryRepository;
      _logger = logger;
    }

    public override async Task<GetAllCategoriesResponse> 
      GetAllCategories(GetAllCategoriesRequest request, ServerCallContext context)
    {
      var all = await _categoryRepository.FetchAllAsync(request.Count);

      var response = new GetAllCategoriesResponse();

      response.Categories.AddRange(all.Select(p => p.MapToMessage()));

      return response;
    }

    public override async Task CreateNewCategory(IAsyncStreamReader<CreateNewCategoryRequest> requestStream, 
      IServerStreamWriter<CreateNewCategoryResponse> responseStream, ServerCallContext context)
    {
      var categoryId = 1;
      while (await requestStream.MoveNext())
      {
        _logger.LogInformation($"New Category {requestStream.Current.CategoryName}.");
        await responseStream.WriteAsync(new CreateNewCategoryResponse {CategoryId = categoryId++});
      }
      _logger.LogInformation($"Last CategoryId {categoryId}.");
    }
  }
}
