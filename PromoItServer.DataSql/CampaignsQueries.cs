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

        public void UpdateCampaignQuery(Campaign data, string userEmail)
        {
            string insertCampaignQuery = "declare @NPO_code int\r\nselect @NPO_code = (select Code from Non_Profit_Organizations where Email='NPO@gmail.com')\r\nupdate Campaigns set Name='"+data.Name+"', Email='"+data.Email+"', Description='"+data.Description+"', Landing_Page_URL='"+data.LandingPageURL+"',\r\nHashTag='"+data.HashTag+"', Image='"+data.MyImage+"' where Code="+data.Code+"";
            PromoItServer.DAL.SqlFunctions.WriteToDB(insertCampaignQuery);
        }

        public DataTable GetCampaignsQuery()
        {
            string CampaignsQuery = "select C.*, NPO.Name as NPO_Name, NPO.Email as NPO_Email, NPO.Website_URL as NPO_Website from Campaigns as C\r\ninner join Non_Profit_Organizations as NPO\r\non C.NPO_code = NPO.Code";
            return PromoItServer.DAL.SqlFunctions.ReadTableFromDB(CampaignsQuery);
        }

        public void CampaignIsActiveQuery(string campaignCode)
        {
            string updateQuery = "update Campaigns set Is_Active = ~Is_Active where Code = '" + campaignCode + "'";
            PromoItServer.DAL.SqlFunctions.WriteToDB(updateQuery);
        }
    }
}
