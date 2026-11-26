using Greet;

namespace GrpcServiceVersioningDemo.Services
{
    public class Helper
    {
        public string ProcessV1Message(HelloRequest request)
        {
            if (request != null)
                return request.FirstName;
            else
                return "Error";
        }

        public string ProcessV2Message(HelloRequest request)
        {
            if (request != null)
                return request.FirstName + " " + request.LastName;
            else
                return "Error";
        }
    }
}
