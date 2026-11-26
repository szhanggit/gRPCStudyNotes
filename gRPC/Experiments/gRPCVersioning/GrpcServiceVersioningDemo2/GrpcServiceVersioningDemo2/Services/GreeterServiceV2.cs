using Greet.V2;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GrpcServiceVersioningDemo2.Services
{
    public class GreeterServiceV2 : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterServiceV2> _logger;
        public GreeterServiceV2(ILogger<GreeterServiceV2> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                //You can see two seperate properties of HelloRequest message
                Message = "Hello " + request.FirstName + " " + request.LastName
            });
        }
    }
}
