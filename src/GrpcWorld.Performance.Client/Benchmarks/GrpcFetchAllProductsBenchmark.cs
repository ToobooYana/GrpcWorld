using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Grpc.Net.Client;
using GrpcWorld.Entities;
using Message;
using Services;

namespace GrpcWorld.Performance.Client.Benchmarks
{
   [SimpleJob(RunStrategy.Throughput, 1, 1, -1, 1, "Grpc.AllProducts")]
    public class GrpcFetchAllProductsBenchmark
    {
        private static readonly Order.OrderClient Client;

        static GrpcFetchAllProductsBenchmark()
        {
            Client ??= CreateClient();
        }

        private static Order.OrderClient CreateClient()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5071", new GrpcChannelOptions
            {
                MaxReceiveMessageSize = 64 * 1024 * 1024, // 64 MB
                MaxSendMessageSize = 64 * 1024 * 1024 // 64 MB
            });

            var client = new Order.OrderClient(channel);

            return client;
        }

        [Params(1, 10, 100, 1000, 10000, 100000)]
        public int Count { get; set; }

        [Benchmark]
        public async Task<List<Product>> AllProducts_with_cold_start()
        {
            var client = CreateClient();

            var products = await FetchProductsCallExample(client);

            return products;
        }

        [Benchmark]
        public async Task<List<Product>> AllProducts_with_warm_start()
        {
            var products = await FetchProductsCallExample(Client);

            return products;
        }

        private async Task<List<Product>> FetchProductsCallExample(Order.OrderClient client)
        {
            var request = new GetAllProductRequest {Count = Count};

            var replies = await client.GetAllProductsAsync(request);

            return replies.Products.ToList();
        }
    }
}