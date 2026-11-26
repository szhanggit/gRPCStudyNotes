using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TXC.Proto.Product;

namespace BackConnectorService2.Extensions
{
    public static class GrpcExtServiceExtension
    {
        public static void AddGrpcExtService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpcClient<Product.ProductClient>(o =>
            {
                o.Address = new Uri(configuration["ServiceUrlConfiguration:ServiceProductUrl"]);
            });
        }
    }
}
