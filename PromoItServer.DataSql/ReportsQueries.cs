using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.DataSql
{
    public class ReportsQueries
    {
        public DataTable GetReportQuery(string type)
        {
            string getReportQuery;
                switch (type)
            {
                case "NPO":
                    getReportQuery = "SELECT Non_Profit_Organizations.Code, Non_Profit_Organizations.Name, Non_Profit_Organizations.Email, Non_Profit_Organizations.Website_URL,\r\nCOUNT(DISTINCT Campaigns.Code) as 'Campaigns', \r\nCOUNT(DISTINCT Products.Code) as 'Products', \r\nCOUNT(DISTINCT Orders.Code) as 'Donations'\r\nFROM Non_Profit_Organizations\r\nLEFT JOIN Campaigns ON Non_Profit_Organizations.Code = Campaigns.NPO_code\r\nLEFT JOIN Products ON Campaigns.Code = Products.Campaign_code\r\nLEFT JOIN Orders ON Campaigns.Code = Orders.Campaign_code\r\nGROUP BY Non_Profit_Organizations.Code, Non_Profit_Organizations.Name, Non_Profit_Organizations.Email, Non_Profit_Organizations.Website_URL";
                    break;

                case "BC":
                    getReportQuery = "SELECT Buisness_Companies.Code, Buisness_Companies.Name, Buisness_Companies.Email,\r\nCOUNT(DISTINCT Products.Code) as 'Products', COUNT(DISTINCT Orders.Code) as 'Donations'\r\nFROM Buisness_Companies\r\nLEFT JOIN Products ON Buisness_Companies.Code = Products.BC_code\r\nLEFT JOIN Orders ON Buisness_Companies.Code = Orders.BC_code\r\nGROUP BY Buisness_Companies.Code, Buisness_Companies.Name, Buisness_Companies.Email";
                    break;

                case "SA":
                    getReportQuery = "SELECT Social_Activist.Code, Social_Activist.Name, Social_Activist.Email, Social_Activist.Address, Social_Activist.Phone_Number, Social_Activist.Money_Status,\r\nCOUNT(DISTINCT Tweets.Code) as 'Social_Activity',\r\nCOUNT(DISTINCT Orders.Code) as 'Donations'\r\nFROM Social_Activist\r\nLEFT JOIN Tweets ON Social_Activist.Code = Tweets.SA_code\r\nLEFT JOIN Orders ON Social_Activist.Code = Orders.SA_code\r\nGROUP BY Social_Activist.Code, Social_Activist.Name, Social_Activist.Email, Social_Activist.Address, Social_Activist.Phone_Number, Social_Activist.Money_Status";
                    break;

                case "campaigns":
                    getReportQuery = "SELECT Campaigns.Code, Campaigns.Name, Campaigns.Email, Campaigns.Landing_Page_URL, Non_Profit_Organizations.Name as 'Organization',\r\nCOUNT(DISTINCT Tweets.Code) as 'Social_Activity', COUNT(DISTINCT Orders.Code) as 'Donations'\r\nFROM Campaigns\r\nLEFT JOIN Non_Profit_Organizations ON Campaigns.NPO_code = Non_Profit_Organizations.Code\r\nLEFT JOIN Tweets ON Campaigns.Code = Tweets.Campaign_code\r\nLEFT JOIN Orders ON Campaigns.Code = Orders.Campaign_code\r\nGROUP BY Campaigns.Code, Campaigns.Name, Campaigns.Email, Campaigns.Landing_Page_URL, Non_Profit_Organizations.Name";
                    break;

                case "tweeter":
                    getReportQuery = "SELECT Tweets.Code, Social_Activist.Name as 'Name', Social_Activist.Email as 'Email', Campaigns.Name as 'Campaign', Tweets.HashTag\r\nFROM Tweets\r\nLEFT JOIN Social_Activist ON Tweets.SA_code = Social_Activist.Code\r\nLEFT JOIN Campaigns ON Tweets.Campaign_code = Campaigns.Code";
                    break;

                default:
                    getReportQuery = "";
                    break;
            }

            return PromoItServer.DAL.SqlFunctions.ReadTableFromDB(getReportQuery);
        }
    }
}
