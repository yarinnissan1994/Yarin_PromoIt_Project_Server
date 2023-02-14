using Newtonsoft.Json.Linq;
using PromoItServer.DataSql;
using PromoItServer.model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Utilities_CS;

namespace PromoItServer.entities
{
    public class TwitterManager : BaseEntity
    {
        public TwitterManager(Log Logger) : base(Logger) 
        {
            twitterQ = new TwitterQueries(Logger);
        }
        TwitterQueries twitterQ;
        public void StartTwitterUpdaterTask()
        {
            Task.Run(TwitterUpdater);
        }
        void TwitterUpdater()
        {
            Log.LogEvent("TwitterUpdater function was called");
            while (true)
            {
                try
                {
                    var request = new RestRequest("", Method.Get);
                    request.AddHeader("authorization", $"Bearer {MainManager.Instance.Config.TwitterBearerCode}");
                    MainManager.Instance.TwitterM.UpdateTweetListAndSAMoney(request);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    Log.LogException("An error occurred on TwitterUpdater function: ", ex);
                }
                Thread.Sleep(3600000);
            }
        }


        public void UpdateTweetListAndSAMoney(RestRequest request)
        {
            Log.LogEvent("UpdateTweetListAndSAMoney function was called");
            List<SocialActivist> SAList = new List<SocialActivist>();
            List<Campaign> CampaignList = new List<Campaign>();
            SAList = twitterQ.GetSAListQuery();
            CampaignList = twitterQ.GetCampaignListQuery();
            RunOnAllSAUsers(SAList, CampaignList, request);
        }

        void RunOnAllSAUsers(List<SocialActivist> SAList, List<Campaign> CampaignList, RestRequest request)
        {
            Log.LogEvent("RunOnAllSAUsers function was called");
            foreach (SocialActivist SA in SAList)
            {
                if (SA.TwitterName[0] == '@')
                {
                    SA.TwitterName = SA.TwitterName.Substring(1);
                }
                RunOnAllCampaigns(SA, CampaignList, request);
            }
        }

        void RunOnAllCampaigns(SocialActivist SA, List<Campaign> CampaignList, RestRequest request)
        {
            Log.LogEvent("RunOnAllCampaigns function was called");
            foreach (Campaign Campaign in CampaignList)
            {
                if (Campaign.HashTag[0] == '#')
                {
                    Campaign.HashTag = Campaign.HashTag.Substring(1);
                }
                var tweetsURL = $"https://api.twitter.com/2/tweets/search/recent?query=from:{SA.TwitterName}%20%23PromoIt%20%23{Campaign.HashTag}";
                var client = new RestClient(tweetsURL);
                var response = client.Execute(request);
                Console.WriteLine(response.Content);
                if (response.IsSuccessful && !response.Content.Contains("\"result_count\":0"))
                {
                    JObject json = JObject.Parse(response.Content);

                    JArray tweets = (JArray)json["data"];

                    UpdateTweetsData(tweets, SA, Campaign);
                }
            }
        }

        void UpdateTweetsData(JArray tweets, SocialActivist SA, Campaign Campaign)
        {
            Log.LogEvent("UpdateTweetsData function was called");
            foreach (var tweet in tweets)
            {
                string tweetId = tweet["id"].ToString();
                string tweetText = tweet["text"].ToString();
                Tweet newTweet = new Tweet();
                newTweet.SACode = SA.Code;
                newTweet.CampaignCode = Campaign.Code;
                newTweet.HashTag = Campaign.HashTag;
                newTweet.LandingPageURL = Campaign.LandingPageURL;
                newTweet.TweetContent = tweetText;
                newTweet.TweetId = tweetId;
                twitterQ.UpdateTweetListAndSAMoneyQuery(newTweet);
            }
        }



    }
}
