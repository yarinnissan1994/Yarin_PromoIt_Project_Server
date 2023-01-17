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
    public class TwitterManager
    {
        public List<SocialActivist> GetSAList()
        {
            TwitterQueries twitterQ = new TwitterQueries();
            return twitterQ.GetSAListQuery();
        }

        public List<Campaign> GetCampaignList()
        {
            TwitterQueries twitterQ = new TwitterQueries();
            return twitterQ.GetCampaignListQuery();
        }

        public void UpdateTweetListAndSAMoney(Tweet newTweet)
        {
            TwitterQueries twitterQ = new TwitterQueries();
            twitterQ.UpdateTweetListAndSAMoneyQuery(newTweet);
        }

    }
}
