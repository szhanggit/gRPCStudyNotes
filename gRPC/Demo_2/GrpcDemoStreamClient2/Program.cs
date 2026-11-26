using Grpc.Core;
using Grpc.Net.Client;
using GrpcDemoStreamService2;
using System;
using System.Threading.Tasks;

namespace GrpcDemoStreamClient2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var customerClient = new Customer.CustomerClient(channel);
            var clientRequested = new CustomerLookupModel { UserId = 1 };
            var customer = await customerClient.GetCustomerInfoAsync(clientRequested);
            Console.WriteLine($"{customer.FirstName} {customer.LastName}");

            Console.WriteLine();
            Console.WriteLine("New Customer List");
            Console.WriteLine();

            using (var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var currentCustomer = call.ResponseStream.Current;

                    Console.WriteLine($"{ currentCustomer.FirstName } { currentCustomer.LastName }: { currentCustomer.EmailAddress }");
                }
            }

            Console.ReadLine();
        }
    }
}
