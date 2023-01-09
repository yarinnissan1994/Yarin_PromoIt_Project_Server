using PromoItServer.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.DataSql
{
    public class CampaignsQueries
    {
        public void NewCampaignQuery(Campaign data, string userEmail)
        {
            string insertCampaignQuery = "declare @NPO_code int\r\nselect @NPO_code = (select Code from Non_Profit_Organizations where Email='"+userEmail+ "')\r\ninsert into Campaigns values ('" + data.Name+"', '"+data.Email+"', '"+data.Description+"', '"+data.LandingPageURL+"', '"+data.HashTag+"', @NPO_code, '"+data.MyImage+"', 1)";
            PromoItServer.DAL.SqlFunctions.WriteToDB(insertCampaignQuery);
        }

        public DataTable GetCampaignsQuery()
        {
            string CampaignsQuery = "select * from Campaigns";
            return PromoItServer.DAL.SqlFunctions.ReadTableFromDB(CampaignsQuery);
        }
    }
}
