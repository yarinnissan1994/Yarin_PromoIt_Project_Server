using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MySqlX.XDevAPI;
using Newtonsoft.Json.Linq;
using PromoItServer.entities;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace PromoItServer.MicroService
{
    public static class MicroService
    {
        [FunctionName("MicroService")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "delete", Route = "service/{action}/{WebVar?}")] HttpRequest req,
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
                    case "NotFoundResult":
                        return new NotFoundResult();
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
