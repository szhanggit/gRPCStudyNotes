using Greet;
using Grpc.Core;
using Grpc.Net.Client;
using System;

namespace ClientWithMetadata
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is metadata carrying version number test");
            var genChannel = GrpcChannel.ForAddress("https://localhost:7259");
            var genClient = new GreeterMultiVerionRpcSet.GreeterMultiVerionRpcSetClient(genChannel);

            //metadata object with version : 1.0 as key value pair
            Metadata metaV1 = new Metadata() {
                { "version", "1.0" }
            };
            //passing metadata object to SayHello procedure
            var replyV1 = genClient.SayHello(new HelloRequest { FirstName = "John", LastName = "Doe" }, metaV1);

            //metadata object with version : 2.0 as key value pair
            Metadata metaV2 = new Metadata() {
                { "version", "2.0" }
            };
            ////passing metadata object to SayHello procedure
            var replyV2 = genClient.SayHello(new HelloRequest { FirstName = "John", LastName = "Doe" }, metaV2);

            Console.WriteLine($"replyV1.Message = {replyV1.Message} | replyV2.Message = {replyV2.Message}");


            Console.WriteLine("Checking use of deprecated field");
            //Trying to pass depricated field in request
            var replyV3 = genClient.SayHello(new HelloRequest { Name = "John Doe", FirstName = "John", LastName = "Doe" }, metaV2);
            Console.WriteLine($"replyV3.Message = {replyV3.Message} | Only warning by C# compiler shown but accepted deprecated field.");


            Console.ReadLine();
        }
    }
}