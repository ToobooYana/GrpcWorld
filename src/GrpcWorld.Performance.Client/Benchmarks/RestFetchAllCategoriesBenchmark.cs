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
   [SimpleJob(RunStrategy.Throughput, 1, 1, -1, 1, "Rest.AllCategories")]
    public class RestFetchAllCategoriesBenchmark
    {
        private HttpClient _client;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public RestFetchAllCategoriesBenchmark()
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
        public async Task<string> AllCategories()
        {
            var categoriesAsJson = await GetAllAsString();
            return categoriesAsJson;
        }

        [Benchmark]
        public async Task<List<Category>> AllCategories_with_deserialization()
        {
            var asJson = await GetAllAsString();

            var categories = JsonSerializer
                .Deserialize<IEnumerable<Models.Category>>(asJson, _jsonSerializerOptions);

            return categories.ToList();
        }

        private async Task<string> GetAllAsString()
        {
            var response = await _client.GetAsync($"/category/{Count}");
            return await response.Content.ReadAsStringAsync();
        }
    }
}