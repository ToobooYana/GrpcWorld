using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Services;

namespace GrpcWorld.Basic.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press a key to continue..");
            Console.ReadKey();

            var channel = GrpcChannel.ForAddress("https://localhost:5051");

            // await FetchProductsExampleCall(channel);
            await CreateCategoriesStreamingExampleCall(channel);
            //await FetchCategoriesExampleCall();


            Console.WriteLine("Press a key to exit..");
            Console.ReadKey();
        }

        public static async Task CreateCategoriesStreamingExampleCall(GrpcChannel channel)
        {
            var client = new Categories.CategoriesClient(channel);

            var count = 1;
            var receivedCount = 0;
            var maxCount = 100000;

            using (var call = client.CreateNewCategory())
            {
                var responseTask = Task.Run(async () =>
                {
                    await foreach (var message in call.ResponseStream.ReadAllAsync())
                    {
                        receivedCount++;
                    }
                });

                while (count < maxCount)
                {
                    try
                    {
                        await call.RequestStream.WriteAsync(new CreateNewCategoryRequest
                        {
                            CategoryName = $"Category #{count++}"
                        });
                    }
                    catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
                    {
                        Console.WriteLine("Cancelled");
                        break;
                    }
                }

                Console.WriteLine("Disconnecting");
                await call.RequestStream.CompleteAsync();
                await responseTask;
            }
        }

        public static async Task FetchCategoriesExampleCall()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5051");
            var client = new Categories.CategoriesClient(channel);
            var request = new GetAllCategoriesRequest();
            var replies = await client.GetAllCategoriesAsync(request);
            var categories = replies.Categories.ToList();
            Console.WriteLine($"Received {categories.Count} categories.");
        }

        private static async Task FetchProductsExampleCall(GrpcChannel channel)
        {
            var client = new Order.OrderClient(channel);

            var request = new GetAllProductRequest();

            var replies = await client.GetAllProductsAsync(request);

            var products = replies.Products.ToList();

            Console.WriteLine($"Received {products.Count} products.");
        }
    }
}
