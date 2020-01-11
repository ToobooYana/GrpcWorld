using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using GrpcWorld.Models;

namespace GrpcWorld.Performance.Client.Benchmarks
{
   [SimpleJob(RunStrategy.Throughput, 1, 1, -1, 1, "Rest.AllProducts")]
    public class RestFetchAllProductsBenchmark
    {
        private HttpClient _client;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public RestFetchAllProductsBenchmark()
        {
            _client = new HttpClient {BaseAddress = new Uri("https://localhost:5075")};

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            };
        }

        [Params(1, 10, 100, 1000, 10000, 100000)]
        public int Count { get; set; }

        [Benchmark]
        public async Task<string> AllProducts()
        {
            var productsAsJson = await GetAllAsString(Count);

            return productsAsJson;
        }

        [Benchmark]
        public async Task<List<Product>> AllProducts_with_deserialization()
        {
            var productsAsJson = await GetAllAsString(Count);

            var products = JsonSerializer
                .Deserialize<IEnumerable<Models.Product>>(productsAsJson, _jsonSerializerOptions);

            return products.ToList();
        }

        private async Task<string> GetAllAsString(int count)
        {
            var response = await _client.GetAsync($"/order/GetProducts/{count}");

            var productsAsJson = await response.Content.ReadAsStringAsync();
            return productsAsJson;
        }
    }
}