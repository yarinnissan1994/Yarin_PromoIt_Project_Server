using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities.Commands
{
    public class Campaigns_get_campaigns : ICommand
    {
        public (string, object) Run(params object[] arg)
        {
            MainManager.Instance.Logger.LogEvent("Campaigns_get_campaigns Command Was called");
            try
            {
                string responseMessage = JsonConvert.SerializeObject(MainManager.Instance.CampaignsM.GetCampaignsFromDB());
                return ("OkObjectResult", responseMessage);
            }
            catch (Exception ex)
            {
                MainManager.Instance.Logger.LogException("Error while getting campaigns: ", ex);
                return ("BadRequestObjectResult", "Error while getting campaigns: " + ex.Message);
            }
        }
        public void Init() { }
    }
}
