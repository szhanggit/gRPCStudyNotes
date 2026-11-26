using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using GrpcServiceExceptionHandlingDemo;
using Google.Protobuf.WellKnownTypes;

namespace GrpcServiceExceptionHandlingDemo.Interceptors
{
    public class ServerExceptionHandlerInterceptor : Interceptor
    {
        private readonly ILogger _logger;

        public ServerExceptionHandlerInterceptor(ILogger<ServerExceptionHandlerInterceptor> logger)
        {
            _logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(request, context);
            }
            catch (Exception ex)
            {
                //Exception logging can be done here
                var responseVm = new ErrorDetailsModel
                {
                    Success = false,
                    Message = $"Internal error occurred - {ex.Message}"
                };
                return MapResponse<TRequest, TResponse>(responseVm);
            }
        }

        /// <summary>
        /// This method maps ErrorDetailsModel values to gRPC service response
        /// </summary>
        /// <param name="responseViewModel">This object holds error details</param>
        /// <returns>gRPC service exception response</returns>
        private TResponse MapResponse<TRequest, TResponse>(ErrorDetailsModel responseViewModel)
        {
            var concreteResponse = Activator.CreateInstance<TResponse>();
            concreteResponse?.GetType().GetProperty(nameof(responseViewModel.Success))?.SetValue(concreteResponse, responseViewModel.Success);
            concreteResponse?.GetType().GetProperty(nameof(responseViewModel.Message))?.SetValue(concreteResponse, responseViewModel.Message);
            return concreteResponse;
        }
    }

    /// <summary>
    /// This model holds error details
    /// </summary>
    public class ErrorDetailsModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
