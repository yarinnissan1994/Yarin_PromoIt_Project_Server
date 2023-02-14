using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;

namespace PromoItServer.entities.Commands
{
    public class Twitter_post_donation_tweet : ICommand
    {
        public (string, object) Run(params object[] arg)
        {
            MainManager.Instance.Logger.LogEvent("Twitter_post_donation_tweet Command Was called");
            string ConsumerKey = MainManager.Instance.Config.ConsumerKey;
            string ConsumerKeySecret = MainManager.Instance.Config.ConsumerKeySecret;
            string AccessKey = MainManager.Instance.Config.AccessKey;
            string AccessKeySecret = MainManager.Instance.Config.AccessKeySecret;

            var userClient = new TwitterClient(ConsumerKey, ConsumerKeySecret, AccessKey, AccessKeySecret);
            try
            {
                RunAsync(userClient, (string)arg[1]);
                return ("success", null);
            }
            catch (Exception ex)
            {
                MainManager.Instance.Logger.LogException("Error while posting donation tweet: ", ex);
                return ("BadRequestObjectResult", "Error while posting donation tweet: " + ex.Message);
            }
        }
        public void Init() { }
        private async void RunAsync(TwitterClient userClient, string requestBody)
        {
            var user = await userClient.Users.GetAuthenticatedUserAsync();
            Console.WriteLine(user);

            dynamic dataD = JsonConvert.DeserializeObject(requestBody);
            if (dataD.Quantity > 1)
            {
                var tweet = await userClient.Tweets.PublishTweetAsync("#PromoIt" + dataD.TwitterName +" just    donated " + dataD.Quantity + " " + dataD.ProductName + "`s to support the " +dataD.CampaignName +   " campaign, thank you for your kind donatinon\nsearch #PromoItand " + dataD.CampaignHashTag + "  for more info!");
                Console.WriteLine("You published the tweet : " + tweet);
            }
            else
            {
                var tweet = await userClient.Tweets.PublishTweetAsync("#PromoIt" + dataD.TwitterName +" just    donated " + dataD.ProductName + " to support the " + dataD.CampaignName + "campaign, thank you     for your kind donatinon\nsearch #PromoIt and " +dataD.CampaignHashTag + " for more info!");
                Console.WriteLine("You published the tweet : " + tweet);
            }
        }
    }
}
