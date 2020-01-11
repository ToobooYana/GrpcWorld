using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Server.Repository.Repositories;

namespace Server.Repository.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var repository = new ProductsRepository(NullLogger<ProductsRepository>.Instance);

            var products = await repository.FetchAllAsync(100);
            
        }
    }
}
