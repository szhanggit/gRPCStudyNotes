using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Greet.V1;

namespace GrpcServiceVersioningDemo2.Services
{
    public class GreeterServiceV1 : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterServiceV1> _logger;
        public GreeterServiceV1(ILogger<GreeterServiceV1> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}