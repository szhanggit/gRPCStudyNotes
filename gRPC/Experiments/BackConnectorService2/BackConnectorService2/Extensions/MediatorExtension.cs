using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Servcies.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackConnectorService2.Extensions
{
    public static class MediatorExtension
    {
        public static void AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(UpdateTxProductStatusCommand).Assembly);
        }
    }
}
