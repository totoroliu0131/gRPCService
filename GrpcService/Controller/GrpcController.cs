using System;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GrpcService.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class GrpcController : ControllerBase
    {
        private readonly ILogger<GrpcController> _logger;

        public GrpcController(ILogger<GrpcController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("Get")]
        public async Task<HelloReply> Get(HelloRequest request)
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