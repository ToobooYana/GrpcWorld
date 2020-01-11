using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using GrpcWorld.Models;

namespace GrpcWorld.Performance.Client.Benchmarks
{
   [SimpleJob(RunStrategy.Throughput, 1, 1, -1, 1, "Rest.OneProduct")]
    public class RestFetchOneProductBenchmark
    {
        private HttpClient _client;
        private int _sequenceProductId = 0;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        private int Next()
        {
            if (_sequenceProductId > 1000)
                return _sequenceProductId = 0;

            return ++_sequenceProductId;
        }

        public RestFetchOneProductBenchmark()
        {
            _client = new HttpClient {BaseAddress = new Uri("https://localhost:5075")};

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            };
        }

        [Benchmark]
        public async Task<string> GetOneById()
        {
            var productsAsJson = await GetOneAsString(Next());

            return productsAsJson;
        }

        [Benchmark]
        public async Task<Product> GetOneById_with_deserialization()
        {
            var oneAsJson = await GetOneAsString(Next());

            var product = JsonSerializer
                .Deserialize<Models.Product>(oneAsJson, _jsonSerializerOptions);

            return product;
        }

        [Benchmark]
        public async Task Ping()
        {
            await _client.GetAsync("/ping");
        }

        private async Task<string> GetOneAsString(int productId)
        {
            var response = await _client.GetAsync($"/order/GetOne/{productId}");

            var productsAsJson = await response.Content.ReadAsStringAsync();
            return productsAsJson;
        }
    }
}