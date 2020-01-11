using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcWorld.Basic.Server.Extensions;
using Server.Repository.Repositories;
using Services;

namespace GrpcWorld.Basic.Server.Services
{
    public class OrderService : Order.OrderBase
    {
        private readonly IProductsRepository _productsRepository;

        public OrderService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public override async Task<GetAllProductResponse> GetAllProducts(GetAllProductRequest request, ServerCallContext context)
        {
            var all = await _productsRepository.FetchAllAsync(request.Count);

            var response = new GetAllProductResponse();

            response.Products.AddRange(all.Select(p => p.MapToMessage()));

            return response;
        }

        public override async Task<GetProductResponse> GetProduct(GetProductRequest request, ServerCallContext context)
        {
            var product = await _productsRepository.GetByIdAsync(request.ProductId);

            var response = new GetProductResponse
            {
                Product = product?.MapToMessage()
            };

            return response;
        }
    }
}
