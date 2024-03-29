using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PromoItServer.entities;
using System;
using System.IO;
using System.Threading.Tasks;
using Tweetinvi;

namespace PromoItServer.MicroService
{
    public static class Twitter
    {
        [FunctionName("Twitter")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "twitter/{action}")] HttpRequest req, string action, ILogger log)
        {
            //MainManager.Instance.Logger.LogEvent($"client called server to execute {action} command");
            try
            {
                string requestBody = await req.ReadAsStringAsync();
                (string response, object result) = MainManager.Instance.CommmandM.CommandList[action].Run(action, requestBody);
                switch (response)
                {
                    case "success":
                        break;
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