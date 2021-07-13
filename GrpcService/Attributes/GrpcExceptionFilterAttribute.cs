using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace GrpcService.Attributes
{
    public class GrpcExceptionInterceptor: Interceptor
    {
        private readonly ILogger<GrpcExceptionInterceptor> _logger;
 
        public GrpcExceptionInterceptor(ILogger<GrpcExceptionInterceptor> logger)
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
            catch (Exception exception)
            {
                _logger.LogInformation(exception.ToString()); 
                throw new RpcException(Status.DefaultCancelled, exception.Message);
            }
        }
    }
}