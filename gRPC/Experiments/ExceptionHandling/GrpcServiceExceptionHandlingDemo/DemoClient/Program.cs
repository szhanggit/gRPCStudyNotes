using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using GrpcServiceExceptionHandlingDemo;
using System;

namespace DemoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var greeterClient = new Greeter.GreeterClient(channel);
            var reply = greeterClient.SayHello(new HelloRequest { Name = "Kiran" });
            Console.WriteLine(reply.Message);
            Console.ReadKey();
        }
    }
}
