using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PromoItServer.entities;
using System;
using System.Threading.Tasks;

namespace PromoItServer.MicroService
{
    public static class Reports
    {
        [FunctionName("Reports")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "delete", Route = "reports/{action}/{WebVar?}")] HttpRequest req,
            string action, string WebVar, ILogger log)
        {
            //MainManager.Instance.Logger.LogEvent($"client called server to execute {action} command");
            try
            {
                (string response, object result) = MainManager.Instance.CommmandM.CommandList[action].Run(action, WebVar);
                switch (response)
                {
                    case "OkObjectResult":
                        return new OkObjectResult(result);
                    case "BadRequestObjectResult":
                        return new BadRequestObjectResult(result);
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                //MainManager.Instance.Logger.LogException($"exception while execute {action} command: ", ex);
            }

            return null;
        }
    }
}


