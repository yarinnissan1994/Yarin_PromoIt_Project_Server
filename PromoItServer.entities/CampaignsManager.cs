using PromoItServer.DataSql;
using PromoItServer.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities
{
    public class CampaignsManager
    {
        public void SendNewCampaignToDB(Campaign data, string userEmail)
        {
            CampaignsQueries campaignsQ = new CampaignsQueries();
            campaignsQ.NewCampaignQuery(data, userEmail);
        }

        public void SendUpdatedCampaignToDB(Campaign data, string userEmail)
        {
            CampaignsQueries campaignsQ = new CampaignsQueries();
            campaignsQ.UpdateCampaignQuery(data, userEmail);
        }

        public DataTable GetCampaignsFromDB()
        {
            CampaignsQueries campaignsQ = new CampaignsQueries();
            return campaignsQ.GetCampaignsQuery();
        }

        public void ToggleCampaignIsActive(string campaignCode)
        {
            CampaignsQueries campaignsQ = new CampaignsQueries();
            campaignsQ.CampaignIsActiveQuery(campaignCode);
        }
    }
}
