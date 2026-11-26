using Greet;
using Grpc.Core;
using GrpcServiceVersioningDemo.Services;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServiceVersioningDemo.Services
{
    public class GreeterMultiVerionRpcSetService : GreeterMultiVerionRpcSet.GreeterMultiVerionRpcSetBase
    {
        private readonly ILogger<GreeterMultiVerionRpcSetService> _logger;
        private readonly Helper _helper;
        public GreeterMultiVerionRpcSetService(ILogger<GreeterMultiVerionRpcSetService> logger)
        {
            _logger = logger;
            _helper = new Helper();
        }

        /// Added in Proto nuGet Version - 1.0.0.8
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            //set default version
            string version = "1.0";

            var reply = new HelloReply();
            //fetching parameters from metadata passed by client 
            var requesterHeader = context.RequestHeaders.FirstOrDefault(e => e.Key == "version");
            if (requesterHeader != null)
                version = requesterHeader.Value;

            //performing business logic based on version number received from client
            if (version == "2.0")
            {
                //this is external business logic provider
                reply.Message = _helper.ProcessV2Message(request);
            }
            else
            {
                reply.Message = _helper.ProcessV1Message(request);
            }

            return Task.FromResult(reply);
        }
    }
}
