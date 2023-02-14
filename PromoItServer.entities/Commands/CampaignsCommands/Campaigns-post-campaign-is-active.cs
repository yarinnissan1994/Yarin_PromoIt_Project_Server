using PromoItServer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities.Commands
{
    public class Campaigns_post_campaign_is_active : ICommand
    {
        public (string, object) Run(params object[] arg)
        {
            MainManager.Instance.Logger.LogEvent("Campaigns_post_campaign_is_active Command Was called");
            try
            {
                MainManager.Instance.CampaignsM.ToggleCampaignIsActive((string)arg[1]);
                return ("success", null);
            }
            catch (Exception ex)
            {
                MainManager.Instance.Logger.LogException("Error while posting campaign is active: ", ex);
                return ("BadRequestObjectResult", "Error while posting campaign is active: " + ex.Message);
            }
        }
        public void Init() { }
    }
}
