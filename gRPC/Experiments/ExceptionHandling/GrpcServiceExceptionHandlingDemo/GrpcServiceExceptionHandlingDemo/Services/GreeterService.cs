using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServiceExceptionHandlingDemo
{
    public class GreeterService : Greeter.GreeterBase
    {
        public override Task<ProtoBaseResponse> SayHello(HelloRequest request, ServerCallContext context)
        {
            if (request.ThrowException)
            { 
                throw new Exception("This is dummy exception");
            }
            return Task.FromResult(new ProtoBaseResponse
            {
                Success = true,
                Message = "Success",
                Data = Any.Pack(new HelloReply
                {
                    Message = "Hello " + request.Name
                })
            });
        }
    }
}
