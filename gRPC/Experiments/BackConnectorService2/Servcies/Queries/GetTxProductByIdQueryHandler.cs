using Common.Domain;
using Common.Services.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Servcies.Queries
{
    public class GetTxProductByIdQueryHandler : IRequestHandlerWrapper<GetTxProductByIdQuery, GetProductByIdResponse>
    {
        private readonly Product.ProductClient _productClient;
        public GetTxProductByIdQueryHandler(Product.ProductClient productClient)
        {
            _productClient = productClient;
        }

        public async Task<Response<GetProductByIdResponse>> Handle(GetTxProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                GetProductByIdRequest grpcRequest = new GetProductByIdRequest()
                {
                    ProductId = request.ProductId,
                    TenantId = 1,
                    TenantName = "TW"
                };
                var response = await _productClient.GetProductByIdAsync(grpcRequest);
                TXC.Proto.Product.GetProductByIdResponse packedData = response.Data == null ? null : response.Data.Unpack<TXC.Proto.Product.GetProductByIdResponse>();
                GetProductByIdResponse data = new GetProductByIdResponse();
                if (response.Success)
                {
                    var temp = packedData.ProductBasicInfo;
                    data.ProductBasicInfo = new ProductBasicInfo()
                    {
                        ProductId = temp.FirstOrDefault().ProductIssuer,
                        ProductIssuer = temp.FirstOrDefault().ProductIssuer,
                        BrandId = temp.FirstOrDefault().BrandId,
                        BrandStory = temp.FirstOrDefault().BrandStory,
                        CanIssue = temp.FirstOrDefault().CanIssue,
                        CreatedBy = temp.FirstOrDefault().CreatedBy,                        
                        CustomerServiceNote = temp.FirstOrDefault().CustomerServiceNote,
                        Description = temp.FirstOrDefault().Description,
                        ExternalProductCode = temp.FirstOrDefault().ExternalProductCode,
                        IsCartVersion = temp.FirstOrDefault().IsCartVersion,
                        IsCurrentVersion = temp.FirstOrDefault().IsCurrentVersion,
                        IsDeferredChild = temp.FirstOrDefault().IsDeferredChild,
                        IsDynamic = temp.FirstOrDefault().IsDynamic,
                        LastUpdatedBy = temp.FirstOrDefault().LastUpdatedBy,
                        Memo = temp.FirstOrDefault().Memo,
                        MultipleSelectionType = temp.FirstOrDefault().MultipleSelectionType,
                        Note = temp.FirstOrDefault().Note,
                        OperationNote = temp.FirstOrDefault().OperationNote,
                        ProductCategory = temp.FirstOrDefault().ProductCategory,
                        ProductCode = temp.FirstOrDefault().ProductCode,
                        ProductName = temp.FirstOrDefault().ProductName,
                        ProductTag = temp.FirstOrDefault().ProductTag,
                        ProductType = temp.FirstOrDefault().ProductType,
                        ProductVersionId = temp.FirstOrDefault().ProductVersionId,
                        ProgramId = temp.FirstOrDefault().ProgramId,
                        RedeemTime = temp.FirstOrDefault().RedeemTime,
                        ReminderId = temp.FirstOrDefault().ReminderId,
                        SalesNote = temp.FirstOrDefault().SalesNote,
                        Status = temp.FirstOrDefault().Status,
                        Version = temp.FirstOrDefault().Version,
                        VoucherIssuerId = temp.FirstOrDefault().VoucherIssuerId
                    };

                }
                return new Response<GetProductByIdResponse>()
                {
                    Success = response.Success,
                    Message = response.Message,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                var result = new Response<GetProductByIdResponse>()
                {
                    Success = false,
                    Message = ex.Message,
                };
                return result;

            }
        }
    }
}
