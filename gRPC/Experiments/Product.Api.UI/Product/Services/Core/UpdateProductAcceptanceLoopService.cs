using Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Common.Domain;
using TXC.Proto.Product;

namespace Services.Core
{
    public class UpdateProductAcceptanceLoopService : IUpdateProductAcceptanceLoopService
    {
        private IDbConnection _dbConnection;

        public async Task<ProtoBaseResponse> UpdateProductAcceptanceLoop(UpdateProductAcceptanceLoopRequest request
            , GetDBConnectionDel getDBConnection
            , CheckProductDel checkProductDel
            , CheckAcceptanceLoopDel checkAcceptanceLoopDel
            , UpdateProductAcceptanceLoopDel updateProductAcceptanceLoopDel
            )
        {
            try
            {
                if (request.TenantId <= 0)
                    return new ProtoBaseResponse() { Success = false, Message = "TenantBasicInfoId header required" };

                if (string.IsNullOrEmpty(request.TenantName))
                    return new ProtoBaseResponse() { Success = false, Message = "TenantName header required" };

                if (request.ProductId <= 0)
                    return new ProtoBaseResponse() { Success = false, Message = "ProductId Invalid request" };

                if (request.AcceptanceLoopId <= 0)
                    return new ProtoBaseResponse() { Success = false, Message = "AcceptanceLoopId required" };

                // initialize db connection
                Response<IDbConnection> conn = await getDBConnection(request.TenantId);

                if (!conn.Success)
                    return new ProtoBaseResponse() { Success = false, Message = "Error in Tenant DB" };

                _dbConnection = conn.Data;

                Tuple<int, int> productChecker = await checkProductDel(request.ProductId, _dbConnection);
                if (productChecker.Item1 == -1)
                {
                    return new ProtoBaseResponse() { Success = false, Message = "Product not found" };
                }

                Tuple<int, int> acceptanceLoopChecker = await checkAcceptanceLoopDel(request.AcceptanceLoopId, _dbConnection);
                if (acceptanceLoopChecker.Item1 == -1)
                {
                    return new ProtoBaseResponse() { Success = false, Message = "AcceptanceLoop not found" };
                }

                int _result = await updateProductAcceptanceLoopDel(request, _dbConnection);

                if (_result == 0)
                {
                    return new ProtoBaseResponse
                    {
                        Success = false,
                        Message = "Nothing will be changed"
                    };
                }
                else if (_result != 1)
                {
                    return new ProtoBaseResponse
                    {
                        Success = false,
                        Message = "Fail"
                    };
                }
                else
                {
                    return new ProtoBaseResponse
                    {
                        Success = true,
                        Message = "Success"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ProtoBaseResponse() { Success = false, Message = ex.Message };
            }
        }
    }
}
