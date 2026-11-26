using Common.Services.Wrappers;
using Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Servcies.Command
{
    public class UpdateTxProductStatusCommandHandler : IRequestHandlerWrapper<UpdateTxProductStatusCommand, int>
    {
        private readonly string _tenantId;
        private readonly string _tenantName;
        private readonly string _TX2UserName;
        private readonly Product.ProductClient _productClient;

        public UpdateTxProductStatusCommandHandler(Product.ProductClient productClient)
        {
            _productClient = productClient;
        }

        public async Task<Response<int>> Handle(UpdateTxProductStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.ProductId == 0)
                {
                    return Response.Fail("ProductId parameter required", 0);
                }

                UpdateProductStatusRequest grpcRequest = new UpdateProductStatusRequest
                {
                    TX2UserName = "",
                    TenantName = "",
                    TenantId = 1,
                    ProductId = request.ProductId,
                    Status = request.Status
                };

                var grpcResponse = await _productClient.UpdateProductStatusAsync(grpcRequest);

                if (grpcResponse.Success)
                    return Response.Success(grpcResponse.Message, grpcResponse.Data);

                return Response.Fail(grpcResponse.Message, grpcResponse.Data);
            }
            catch (Exception exception)
            {
                return Response.Fail(exception.Message, 0);
            }
        }
    }
}
