using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcWorld.Performance.Server.Grpc.Extensions;
using Microsoft.Extensions.Logging;
using Server.Repository.Repositories;
using Services;

namespace GrpcWorld.Performance.Server.Grpc.Services
{
    public class OrderService : Order.OrderBase
    {
        //private readonly ILogger<OrderService> _logger;
        private readonly IProductsRepository _productsRepository;
        private static readonly Empty Empty = new Empty();

        public OrderService(
          //ILogger<OrderService> logger,
            IProductsRepository productsRepository)
        {
      //_logger = logger;
      _productsRepository = productsRepository;
        }

        public override async Task<GetAllProductResponse> GetAllProducts(GetAllProductRequest request, 
            ServerCallContext context)
        {
            //_logger.LogInformation($"Fetch all.'");

            var all = await _productsRepository.FetchAllAsync(request.Count);

            var response = new GetAllProductResponse();

            response.Products.AddRange(all.Select(p => p.MapToMessage()));

            return response;
        }

        public override async Task<GetProductResponse> GetProduct(GetProductRequest request, ServerCallContext context)
        {
            //_logger.LogInformation($"Fetch productId '{request.ProductId}'");

            var product = await _productsRepository.GetByIdAsync(request.ProductId);

            var response = new GetProductResponse
            {
                Product = product?.MapToMessage()
            };

            return response;
        }

        public override Task<Empty> Ping(Empty request, ServerCallContext context)
        {
          //_logger.LogInformation("Ping");
            return Task.FromResult(Empty);
        }
    }
}
