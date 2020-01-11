using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Grpc.Net.Client;
using GrpcWorld.Entities;
using Services;

namespace GrpcWorld.Performance.Client.Benchmarks
{
   [SimpleJob(RunStrategy.Throughput, 1, 1, -1, 1, "Grpc.AllCategories")]
    public class GrpcFetchAllCategoriesBenchmark
    {
        private static readonly Categories.CategoriesClient Client;

        static GrpcFetchAllCategoriesBenchmark()
        {
            Client ??= CreateClient();
        }

        private static Categories.CategoriesClient CreateClient()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5071");

            var client = new Categories.CategoriesClient(channel);

            return client;
        }

        [Params(1, 10, 100, 1000, 10000, 100000)]
        public int Count { get; set; }

        [Benchmark]
        public async Task<List<Category>> AllCategories_with_warm_start()
        {
            var request = new GetAllCategoriesRequest {Count = Count};

            var replies = await Client.GetAllCategoriesAsync(request);

            return replies.Categories.ToList();
        }
    }
}