using System;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;
using GrpcWorld.Performance.Client.Benchmarks;

namespace GrpcWorld.Performance.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("press key to start.");
            Console.ReadKey();

            BenchmarkSwitcher
                .FromAssembly(typeof(Program).Assembly)
                .RunAllJoined();

            return;

            //var restClient = new RestFetchAllProductsBenchmark();
            //restClient.Count = 100000;
            //var json = await restClient.AllProducts();
            //var entries = await restClient.AllProducts_with_deserialization();

            //Console.WriteLine("press key to continue.");
            //Console.ReadKey();

            //var grpcClient = new GrpcFetchAllCategoriesBenchmark();
            //grpcClient.Count = 100000;
            //var categories = await grpcClient.AllCategories_with_warm_start();

            //var grpcClient2 = new GrpcFetchAllProductsBenchmark();
            //grpcClient2.Count = 100000;
            //var products = await grpcClient2.AllProducts_with_warm_start();


            //Console.WriteLine("ready.");
            //Console.ReadKey();
        }
    }
}
