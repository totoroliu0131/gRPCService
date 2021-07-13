using System;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcService
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;

        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            var milliseconds = (Timestamp.FromDateTime(DateTime.Now.ToUniversalTime())-request.LogDate).ToTimeSpan().Milliseconds;
            return new HelloReply
            {
                Message = "Hello " + request.Name,
                Milliseconds = milliseconds
            };
        }
    }
}