using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Message;
using Services;

namespace GrpcWorld.Performance.Client.Benchmarks
{
    [SimpleJob(RunStrategy.Throughput, 1, 1, -1, 1, "Grpc.OneProduct")]
    public class GrpcFetchOneProductBenchmark
  {
    private static readonly Order.OrderClient Client;
    private static Empty _empty = new Empty();
    private int _sequenceProductId = 0;

    private int Next()
    {
      if (_sequenceProductId > 1000)
        return _sequenceProductId = 0;

      return ++_sequenceProductId;
    }

    static GrpcFetchOneProductBenchmark()
    {
      Client = CreateClient();
    }

    private static Order.OrderClient CreateClient()
    {
      var channel = GrpcChannel.ForAddress("https://localhost:5071");

      var client = new Order.OrderClient(channel);

      return client;
    }

    [Benchmark]
    public async Task<Product> GetProductById_with_warm_start()
    {
      var product = await GetProductByIdCallExample(Client, Next());

      return product;
    }

    [Benchmark]
    public async Task Ping()
    {
      await Client.PingAsync(_empty);
    }


    private async Task<Product> GetProductByIdCallExample(Order.OrderClient client, int productId)
    {
      var request = new GetProductRequest { ProductId = productId };

      var replies = await client.GetProductAsync(request);

      return replies.Product;
    }
  }
}