using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcWorld.Performance.Server.Grpc.Extensions;
using Server.Repository.Repositories;
using Services;

namespace GrpcWorld.Performance.Server.Grpc.Services
{
    public class CategoryService : Categories.CategoriesBase
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public override async Task<GetAllCategoriesResponse> GetAllCategories(GetAllCategoriesRequest request,
            ServerCallContext context)
        {
            var all = await _repository.FetchAllAsync(request.Count);

            var response = new GetAllCategoriesResponse();

            response.Categories.AddRange(all.Select(c => c.MapToMessage()));

            return response;
        }

        public override async Task GetAllCategoriesWithStream(GetAllCategoriesRequest request, 
            IServerStreamWriter<GetAllCategoriesWithStreamResponse> responseStream, ServerCallContext context)
        {
            foreach (var c in await _repository.FetchAllAsync(request.Count))
            {
                await responseStream.WriteAsync(new GetAllCategoriesWithStreamResponse {Category = c.MapToMessage()});
            }
        }
    }
}
