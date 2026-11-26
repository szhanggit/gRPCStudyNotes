using Grpc.Core;
using Grpc.Net.Client;
using GrpcDemoStreamService;
using System;
using System.Threading.Tasks;

namespace GrpcDemoStreamClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var input = new HelloRequest { Name = "Steven" };
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);

            var reply = await client.SayHelloAsync(input);
            Console.WriteLine(reply.Message);

            //var call = client.SayHelloStreaming(new HelloRequest { Name = "Hello Steven Streaming" });
            //await foreach (var message in call.ResponseStream.ReadAllAsync())
            //{
            //    Console.WriteLine(message.Message);
            //}

            Console.ReadLine();
        }
    }
}
