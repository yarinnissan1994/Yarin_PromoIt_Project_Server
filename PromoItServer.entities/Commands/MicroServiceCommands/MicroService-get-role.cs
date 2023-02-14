using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities.Commands
{
    public class MicroService_get_role : ICommand
    {
        public (string, object) Run(params object[] arg) 
        {
            MainManager.Instance.Logger.LogEvent("MicroService_get_role Command Was called");
            try
            {
                var rolesURL = $"https://dev-71vlxms0epcj8hhj.us.auth0.com/api/v2/users/{arg[1]}/roles";
                string bearerCode = MainManager.Instance.Config.Auth0BearerCode;
                var client = new RestClient(rolesURL);
                var request = new RestRequest("", Method.Get);
                request.AddHeader("authorization", $"Bearer {bearerCode}");
                var response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    var json = JArray.Parse(response.Content);
                    return ("OkObjectResult", json);
                    //return new OkObjectResult(json);
                }
                else
                {
                    return ("NotFoundResult", null);
                    //return new NotFoundResult();
                }
            }
            catch (Exception ex)
            {
                MainManager.Instance.Logger.LogException("Error while getting role: ", ex);
                return ("BadRequestObjectResult", "Error while getting role: " + ex.Message);
                //return new BadRequestObjectResult("Error while getting role: " + ex.Message);
            }
        }
        public void Init() { }
    }
}
