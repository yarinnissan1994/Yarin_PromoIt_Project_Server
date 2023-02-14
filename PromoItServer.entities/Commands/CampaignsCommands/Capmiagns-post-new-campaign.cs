using Newtonsoft.Json;
using PromoItServer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PromoItServer.entities.Commands
{
    public class Capmiagns_post_new_campaign : ICommand
    {
        public (string, object) Run(params object[] arg)
        {
            MainManager.Instance.Logger.LogEvent("Capmiagns_post_new_campaign Command Was called");
            try
            {
                object data = new Campaign();
                data = System.Text.Json.JsonSerializer.Deserialize<Campaign>((string)arg[2]);
                MainManager.Instance.CampaignsM.SendNewCampaignToDB((Campaign)data, (string)arg[1]);
                return ("success", null);
            }
            catch (Exception ex)
            {
                MainManager.Instance.Logger.LogException("Error while posting new campaign: ", ex);
                return ("BadRequestObjectResult", "Error while posting new campaign: " + ex.Message);
            }
        }
        public void Init() { }
    }
}
