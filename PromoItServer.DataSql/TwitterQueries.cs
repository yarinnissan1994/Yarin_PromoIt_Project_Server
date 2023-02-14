using PromoItServer.model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Utilities_CS;

namespace PromoItServer.DataSql
{
    public class TwitterQueries : BaseDataSql
    {
        public TwitterQueries(Log Logger) : base(Logger) { }
        List<SocialActivist> ReadSAListFromDb(SqlDataReader reader)
        {
            Log.LogEvent("ReadSAListFromDb function was called");
            List<SocialActivist> SAList = new List<SocialActivist>();

            while (reader.Read())
            {
                SocialActivist row = new SocialActivist();
                row.Code = reader.GetInt32(0);
                row.TwitterName = reader.GetString(7);

                SAList.Add(row);
            }
            return SAList;
        }
        public List<SocialActivist> GetSAListQuery()
        {
            Log.LogEvent("GetSAListQuery function was called");
            string SqlQuery = "select * from Social_Activist";
            List<SocialActivist> retList = (List<SocialActivist>) PromoItServer.DAL.SqlFunctions.ReadFromDB(SqlQuery, ReadSAListFromDb);
            return retList;
        }

        List<Campaign> ReadCampaignListFromDb(SqlDataReader reader)
        {
            Log.LogEvent("ReadCampaignListFromDb function was called");
            List<Campaign> CampaignList = new List<Campaign>();

            while (reader.Read())
            {
                Campaign row = new Campaign();
                row.Code = reader.GetInt32(0);
                row.LandingPageURL = reader.GetString(4);
                row.HashTag = reader.GetString(5);

                CampaignList.Add(row);
            }
            return CampaignList;
        }
        public List<Campaign> GetCampaignListQuery()
        {
            Log.LogEvent("GetCampaignListQuery function was called");
            string SqlQuery = "select * from Campaigns";
            List<Campaign> retList = (List<Campaign>)PromoItServer.DAL.SqlFunctions.ReadFromDB(SqlQuery, ReadCampaignListFromDb);
            return retList;
        }
        public void UpdateTweetListAndSAMoneyQuery(Tweet newTweet)
        {
            Log.LogEvent("UpdateTweetListAndSAMoneyQuery function was called");
            string updateQuery = "if not exists(select Code from Tweets where Tweet_id like '"+newTweet.TweetId+"')\r\n\tbegin\r\n\t\tinsert into Tweets values ("+newTweet.SACode+", "+newTweet.CampaignCode+", '"+newTweet.HashTag+"', '"+newTweet.LandingPageURL+"', '"+newTweet.TweetContent+"', getdate(), '"+newTweet.TweetId+"')\r\n\t\tupdate Social_Activist set Money_Status = Money_Status + 1 where Code = "+newTweet.SACode+"\r\n\tend";
            PromoItServer.DAL.SqlFunctions.WriteToDB(updateQuery);
        }
        
    }
}
