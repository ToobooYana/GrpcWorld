using System;
using Microsoft.Extensions.DependencyInjection;

namespace Server.Repository
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSingleton<Repositories.IProductsRepository, Repositories.ProductsRepository>();
            services.AddSingleton<Repositories.ICategoryRepository, Repositories.CategoryRepository>();

            return services;
        }
    }
}
