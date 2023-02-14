using PromoItServer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities.Commands
{
    public class Campaigns_post_updated_campaign : ICommand
    {
        public (string, object) Run(params object[] arg)
        {
            MainManager.Instance.Logger.LogEvent("Campaigns_post_updated_campaign Command Was called");
            try
            {
                object data = new Campaign();
                data = System.Text.Json.JsonSerializer.Deserialize<Campaign>((string)arg[2]);
                MainManager.Instance.CampaignsM.SendUpdatedCampaignToDB((Campaign)data);
                return ("success", null);
            }
            catch (Exception ex)
            {
                MainManager.Instance.Logger.LogException("Error while posting updated campaign: ", ex);
                return ("BadRequestObjectResult", "Error while posting updated campaign: " + ex.Message);
            }
        }
        public void Init() { }
    }
}
