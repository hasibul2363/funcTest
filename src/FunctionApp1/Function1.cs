using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FunctionApp1
{
    public class Function1
    {
        private readonly EnvironmentInfo _environmentInfo;
        private readonly AuthenticationSettings _authenticationl;
        public Function1(IOptions<AuthenticationSettings> options, EnvironmentInfo environment)
        {
            _authenticationl = options.Value;
            _environmentInfo = environment;
        }

        [FunctionName("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
           
            return new OkObjectResult($"It works with ENV: {_environmentInfo.Environment} : {JsonConvert.SerializeObject(_authenticationl)})");
        }
    }
}
