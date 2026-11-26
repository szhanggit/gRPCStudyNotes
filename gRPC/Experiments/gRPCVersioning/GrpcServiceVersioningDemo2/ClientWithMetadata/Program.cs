using Grpc.Core;
using Grpc.Net.Client;
//using Greet.V1;
using Greet.V2;

/*
gRPC supports an optional package specifier, which functions much like a .NET namespace. 
In fact, the package will be used as the .NET namespace for generated .NET types if option csharp_namespace is not set in the .proto file. 
The package can be used to specify a version number for your service and its messages. 
 */

namespace ClientWithMetadata
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("I am old client using V1 grpc service.");
            var channel = GrpcChannel.ForAddress("https://localhost:7061");
            var greeterClient = new Greeter.GreeterClient(channel);

            //You can see, Name is the only property in HelloRequest message
            //var reply = greeterClient.SayHello(new HelloRequest { Name = "Kiran" });
            var reply = greeterClient.SayHello(new HelloRequest { FirstName = "Kiran", LastName = "Sarode" });

            Console.WriteLine(reply.Message);

            Console.ReadLine();
        }
    }
}