using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceExceptionHandlingDemo;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServiceExceptionHandlingDemo2
{
    public class GreeterService2 : Greeter2.Greeter2Base
    {
        public override Task<ProtoBaseResponse> SayHello2(HelloRequest2 request, ServerCallContext context)
        {
            if (request.ThrowException)
            {
                throw new IndexOutOfRangeException();
            }
            return Task.FromResult(new ProtoBaseResponse
            {
                Success = true,
                Message = "Success",
                Data = Any.Pack(new HelloReply2
                {
                    Message = "Hello " + request.Name
                })
            });
        }
    }
}
