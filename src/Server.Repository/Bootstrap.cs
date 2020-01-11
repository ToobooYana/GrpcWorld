using System;
using Microsoft.Extensions.DependencyInjection;

namespace Server.Repository
{
    public static class Bootstrap
    {
        /// <summary>
        /// Initialisiert Repository mit Testobjekten. Nur in der Demo-App sinnvoll.
        /// </summary>
        /// <param name="provider"></param>
        public static void Run(IServiceProvider provider)
        {
            var productRepository = provider.GetService<Repositories.IProductsRepository>();
            var categoryRepository = provider.GetService<Repositories.ICategoryRepository>();
        }
    }
}