using System;
using Microsoft.Extensions.DependencyInjection;

namespace GrpcWorld.Performance.Server.Grpc
{
    public static class InternalBootstrap
    {
        /// <summary>
        /// Initialisiert Repository mit Testobjekten. Nur in der Demo-App sinnvoll.
        /// </summary>
        /// <param name="provider"></param>
        public static void Run(IServiceProvider provider)
        {
            var internalRepsository = provider.GetService<Services.IInternalCategoryRepository>();
            internalRepsository.Initialize(100000);
        }
    }
}