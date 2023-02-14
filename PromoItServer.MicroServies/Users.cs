using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PromoItServer.entities;
using PromoItServer.model;
using System;
using System.Threading.Tasks;

namespace PromoItServer.MicroService
{
    public static class Users
    {
        [FunctionName("Users")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "delete", Route = "users/{action}/{WebVar?}/{WebVar2?}")] HttpRequest req,
            string action, string WebVar, string WebVar2, ILogger log)
        {
            //MainManager.Instance.Logger.LogEvent($"client called server to execute {action} command");
            try
            {
                string requestBody = await req.ReadAsStringAsync();
                (string response, object result) = MainManager.Instance.CommmandM.CommandList[action].Run(action, WebVar, requestBody);
                switch (response)
                {
                    case "success":
                        break;
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
            //string responseMessage;

            //object data;

            //switch (action)
            //{
                //case "post-user-create":
                //    try
                //    {
                //        if (WebVar == "NPO")
                //        {
                //            data = new NonProfitOrganization();
                //            data = System.Text.Json.JsonSerializer.Deserialize<NonProfitOrganization>(req.Body);
                //            MainManager.Instance.UsersM.SendUserToDB((NonProfitOrganization)data);
                //            break;
                //        }
                //        else if (WebVar == "BC")
                //        {
                //            data = new BuisnessCompany();
                //            data = System.Text.Json.JsonSerializer.Deserialize<BuisnessCompany>(req.Body);
                //            MainManager.Instance.UsersM.SendUserToDB((BuisnessCompany)data);
                //            break;
                //        }
                //        else
                //        {
                //            data = new SocialActivist();
                //            data = System.Text.Json.JsonSerializer.Deserialize<SocialActivist>(req.Body);
                //            MainManager.Instance.UsersM.SendUserToDB((SocialActivist)data);
                //            break;
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        return new BadRequestObjectResult("Error while posting user create: " + ex.Message);
                //    }

                //case "get-user-info":
                //    try
                //    {
                //        responseMessage = JsonConvert.SerializeObject(MainManager.Instance.UsersM.getUserInfoFromDB(WebVar, WebVar2));
                //        return new OkObjectResult(responseMessage);
                //    }
                //    catch (Exception ex)
                //    {
                //        return new BadRequestObjectResult("Error while getting user info: " + ex.Message);
                //    }

                //case "get-pendding":
                //    try
                //    {
                //        responseMessage = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.UsersM.getPenddingFromDB(WebVar));
                //        return new OkObjectResult(responseMessage);
                //    }
                //    catch (Exception ex)
                //    {
                //        return new BadRequestObjectResult("Error while getting pennding: " + ex.Message);
                //    }

            //    case "get-pendding-list":
            //        try
            //{
            //    responseMessage = JsonConvert.SerializeObject(MainManager.Instance.UsersM.GetPenddingListFromDB());
            //    return new OkObjectResult(responseMessage);
            //}
            //catch (Exception ex)
            //{
            //    return new BadRequestObjectResult("Error while getting pendding list: " + ex.Message);
            //}

            //case "get-my-donations":
            //    try
            //    {
            //        responseMessage = JsonConvert.SerializeObject(MainManager.Instance.UsersM.GetSADonationsFromDB(WebVar));
            //        return new OkObjectResult(responseMessage);
            //    }
            //    catch (Exception ex)
            //    {
            //        return new BadRequestObjectResult("Error while getting my donations: " + ex.Message);
            //    }

            //case "post-approve-user":
            //    try
            //    {
            //        MainManager.Instance.UsersM.ApproveUserInDB(WebVar);
            //        break;
            //    }
            //    catch (Exception ex)
            //    {
            //        return new BadRequestObjectResult("Error while posting approve user: " + ex.Message);
            //    }

            //case "post-user-message":
            //    try
            //    {
            //        data = new UserMessage();
            //        data = System.Text.Json.JsonSerializer.Deserialize<UserMessage>(req.Body);
            //        MainManager.Instance.UsersM.SendUserMessageToDB((UserMessage)data);
            //    }
            //    catch (Exception ex)
            //    {
            //        return new BadRequestObjectResult("Error while posting user message: " + ex.Message);
            //    }
            //    break;

            //    case "post-sa-money-status":
            //        try
            //        {
            //            data = System.Text.Json.JsonSerializer.Deserialize<decimal>(req.Body);
            //            MainManager.Instance.UsersM.SendMoneyStatusToDB((decimal)data, int.Parse(WebVar));
            //        }
            //        catch (Exception ex)
            //        {
            //            return new BadRequestObjectResult("Error while posting sa money status: " + ex.Message);
            //        }
            //        break;

            //    default:
            //        break;

            //}
            return null;
        }
    }
}


