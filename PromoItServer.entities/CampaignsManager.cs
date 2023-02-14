using PromoItServer.DataSql;
using PromoItServer.model;
using System.Data;
using System.Threading.Tasks;
using Utilities_CS;

namespace PromoItServer.entities
{
    public class CampaignsManager : BaseEntity
    {
        public CampaignsManager(Log Logger) : base(Logger) { campaignsQ = new CampaignsQueries(Logger); }
        CampaignsQueries campaignsQ;
        public void SendNewCampaignToDB(Campaign data, string userEmail)
        {
            Log.LogEvent("SendNewCampaignToDB function was called");
            campaignsQ.NewCampaignQuery(data, userEmail);
        }

        public void SendUpdatedCampaignToDB(Campaign data)
        {
            Log.LogEvent("SendUpdatedCampaignToDB function was called");
            campaignsQ.UpdateCampaignQuery(data);
        }

        public DataTable GetCampaignsFromDB()
        {
            Log.LogEvent("GetCampaignsFromDB function was called");
            return campaignsQ.GetCampaignsQuery();
        }

        public void ToggleCampaignIsActive(string campaignCode)
        {
            Log.LogEvent("GetCampaignsFromDB function was called");
            campaignsQ.CampaignIsActiveQuery(campaignCode);
        }
    }
}
