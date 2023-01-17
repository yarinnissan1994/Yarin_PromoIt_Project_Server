using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Tweetinvi;
using PromoItServer.entities;
using PromoItServer.model;
using Newtonsoft.Json.Linq;
using RestSharp;
using Tweetinvi.Core.Events;
using System.Data;
using System.Linq;
using MySqlX.XDevAPI.Relational;
using System.Collections.Generic;

namespace PromoItServer.MicroService
{
    public static class Twitter
    {
        [FunctionName("Twitter")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "twitter/{action}/{WebVar?}")] HttpRequest req, string action, string WebVar, ILogger log)
        {
            object data;
            dynamic dataD;

            var userClient = new TwitterClient("VZ5VkgFepx1My7TxmoWaXjCr6", "q8ufmEXGaow8YJq6cGXynFrDG4ilcvV1ee89PByGUEqLNoFPmy", "1614610924983259146-weskDlwL3A8IjV03n1Q902PLDhuMtA", "ZRwxVUQARSWcTpXhi9BGIimWrJV40Dmul9NPLdHMR1gXJ");

            var user = await userClient.Users.GetAuthenticatedUserAsync();
            Console.WriteLine(user);

            switch (action)
            {
                //case "get-twitter-updater":
                //    List<SocialActivist> SAList = new List<SocialActivist>();
                //    List<Campaign> CampaignList = new List<Campaign>();
                //    SAList = MainManager.Instance.TwitterM.GetSAList();
                //    CampaignList = MainManager.Instance.TwitterM.GetCampaignList();
                //    var tweetsURL = $"https://api.twitter.com/2/tweets/search/recent?query=from:daly_kaza%20%23PromoIt%20%23TEAMTREES";
                //    var client = new RestClient(tweetsURL);
                //    var request = new RestRequest("", Method.Get);
                //    request.AddHeader("authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAAbxlAEAAAAAYfWfIWGOCYsxgb8QWKbVHIILg3g%3DmaB2S5kHwMjRBIuBSdpItgeOylqB5lkYxV5mltg8FPXLNKX0Ht");
                //    var response = client.Execute(request);
                //    Console.WriteLine(response.Content);
                //    if (response.IsSuccessful)
                //    {
                //        JObject json = JObject.Parse(response.Content);

                //        JArray tweets = (JArray)json["data"];

                //        foreach (var tweet in tweets)
                //        {
                //            // Get the user ID
                //            string tweetId = tweet["id"].ToString();
                //            // Get the tweet text
                //            string tweetText = tweet["text"].ToString();

                //            // You can store this data in separate variables or in a class or struct
                //        }
                //        //var json = JArray.Parse(response.Content);
                //        return new OkObjectResult(json);
                //    }
                //    else
                //    {
                //        return new NotFoundResult();
                //    }

                case "get-twitter-updater":
                    List<SocialActivist> SAList = new List<SocialActivist>();
                    List<Campaign> CampaignList = new List<Campaign>();
                    SAList = MainManager.Instance.TwitterM.GetSAList();
                    CampaignList = MainManager.Instance.TwitterM.GetCampaignList();
                    foreach (SocialActivist SA in SAList)
                    {
                        foreach (Campaign Campaign in CampaignList)
                        {
                            if (SA.TwitterName[0] == '@')
                            {
                                SA.TwitterName = SA.TwitterName.Substring(1);
                            }
                            if (Campaign.HashTag[0] == '#')
                            {
                                Campaign.HashTag = Campaign.HashTag.Substring(1);
                            }
                            var tweetsURL = $"https://api.twitter.com/2/tweets/search/recent?query=from:{SA.TwitterName}%20%23PromoIt%20%23{Campaign.HashTag}";
                            var client = new RestClient(tweetsURL);
                            var request = new RestRequest("", Method.Get);
                            request.AddHeader("authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAAbxlAEAAAAAYfWfIWGOCYsxgb8QWKbVHIILg3g%3DmaB2S5kHwMjRBIuBSdpItgeOylqB5lkYxV5mltg8FPXLNKX0Ht");
                            var response = client.Execute(request);
                            Console.WriteLine(response.Content);
                            if (response.IsSuccessful && !response.Content.Contains("\"result_count\":0"))
                            {
                                JObject json = JObject.Parse(response.Content);

                                JArray tweets = (JArray)json["data"];

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
                                    MainManager.Instance.TwitterM.UpdateTweetListAndSAMoney(newTweet);
                                }
                            }
                        }
                    }
                    return null;

                case "post-donation-tweet":
                    dataD = JsonConvert.DeserializeObject(await new StreamReader(req.Body).ReadToEndAsync());
                    if(dataD.Quantity > 1)
                    {
                         var tweet = await userClient.Tweets.PublishTweetAsync("#PromoIt" + dataD.TwitterName + " just donated " + dataD.Quantity + " " + dataD.ProductName + "`s to support the " + dataD.CampaignName + " campaign, thank you for your kind donatinon\nsearch #PromoIt and " + dataD.CampaignHashTag + " for more info!");
                         Console.WriteLine("You published the tweet : " + tweet);
                    }
                    else
                    {
                        var tweet = await userClient.Tweets.PublishTweetAsync("#PromoIt" + dataD.TwitterName + " just donated " + dataD.ProductName + " to support the " + dataD.CampaignName + " campaign, thank you for your kind donatinon\nsearch #PromoIt and " + dataD.CampaignHashTag + " for more info!");
                        Console.WriteLine("You published the tweet : " + tweet);
                    }

                    break;

                default:
                    break;
            }
            //var tweet = await userClient.Tweets.PublishTweetAsync("#PromoIt Ba Rabbak");
            //Console.WriteLine("You published the tweet : " + tweet);

            return null;
        }
    }
}

