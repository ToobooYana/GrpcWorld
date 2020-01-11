using GrpcWorld.Performance.Server.Grpc.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Repository;

namespace GrpcWorld.Performance.Server.Grpc
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc(options =>
                {
                    options.EnableDetailedErrors = true;
                    options.MaxReceiveMessageSize = 64 * 1024 * 1024; // 64 MB
                    options.MaxSendMessageSize = 64 * 1024 * 1024; // 64 MB
                })
                .Services
                .AddRepository();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<CategoryService>();
                endpoints.MapGrpcService<OrderService>();
            });
        }
    }
}
