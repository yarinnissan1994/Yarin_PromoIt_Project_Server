using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Reflection;
using RestSharp;
using Newtonsoft.Json.Linq;
using PromoItServer.entities;
using PromoItServer.model;
using Tweetinvi;
using System.Text.Json;
using Tweetinvi.Core.Models;
using MySqlX.XDevAPI;
using System.Collections.Generic;
using System.Net.Http;
using Tweetinvi.Models;

namespace PromoItServer.MicroService
{
    public static class MicroService
    {
        [FunctionName("MicroService")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "delete", Route = "service/{action}/{WebVar?}/{WebVar2?}")] HttpRequest req,
            string action, string WebVar, string WebVar2, ILogger log)
        {
            string requestBody;

            string responseMessage;

            object data;

            switch (action)
            {
                case "get-role":
                    var rolesURL = $"https://dev-71vlxms0epcj8hhj.us.auth0.com/api/v2/users/{WebVar}/roles";
                    var client = new RestClient(rolesURL);
                    var request = new RestRequest("", Method.Get);
                    request.AddHeader("authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6ImtkUzN4NlRyaDljSUozT2tSMU9IUSJ9.eyJpc3MiOiJodHRwczovL2Rldi03MXZseG1zMGVwY2o4aGhqLnVzLmF1dGgwLmNvbS8iLCJzdWIiOiJISGs3UWtMendaaWRiMHd1ZXpoQ1NSQndsOXpONmdDMUBjbGllbnRzIiwiYXVkIjoiaHR0cHM6Ly9kZXYtNzF2bHhtczBlcGNqOGhoai51cy5hdXRoMC5jb20vYXBpL3YyLyIsImlhdCI6MTY3Mjc4NjgwMCwiZXhwIjoxNjc1Mzc4ODAwLCJhenAiOiJISGs3UWtMendaaWRiMHd1ZXpoQ1NSQndsOXpONmdDMSIsInNjb3BlIjoicmVhZDpjbGllbnRfZ3JhbnRzIGNyZWF0ZTpjbGllbnRfZ3JhbnRzIGRlbGV0ZTpjbGllbnRfZ3JhbnRzIHVwZGF0ZTpjbGllbnRfZ3JhbnRzIHJlYWQ6dXNlcnMgdXBkYXRlOnVzZXJzIGRlbGV0ZTp1c2VycyBjcmVhdGU6dXNlcnMgcmVhZDp1c2Vyc19hcHBfbWV0YWRhdGEgdXBkYXRlOnVzZXJzX2FwcF9tZXRhZGF0YSBkZWxldGU6dXNlcnNfYXBwX21ldGFkYXRhIGNyZWF0ZTp1c2Vyc19hcHBfbWV0YWRhdGEgcmVhZDp1c2VyX2N1c3RvbV9ibG9ja3MgY3JlYXRlOnVzZXJfY3VzdG9tX2Jsb2NrcyBkZWxldGU6dXNlcl9jdXN0b21fYmxvY2tzIGNyZWF0ZTp1c2VyX3RpY2tldHMgcmVhZDpjbGllbnRzIHVwZGF0ZTpjbGllbnRzIGRlbGV0ZTpjbGllbnRzIGNyZWF0ZTpjbGllbnRzIHJlYWQ6Y2xpZW50X2tleXMgdXBkYXRlOmNsaWVudF9rZXlzIGRlbGV0ZTpjbGllbnRfa2V5cyBjcmVhdGU6Y2xpZW50X2tleXMgcmVhZDpjb25uZWN0aW9ucyB1cGRhdGU6Y29ubmVjdGlvbnMgZGVsZXRlOmNvbm5lY3Rpb25zIGNyZWF0ZTpjb25uZWN0aW9ucyByZWFkOnJlc291cmNlX3NlcnZlcnMgdXBkYXRlOnJlc291cmNlX3NlcnZlcnMgZGVsZXRlOnJlc291cmNlX3NlcnZlcnMgY3JlYXRlOnJlc291cmNlX3NlcnZlcnMgcmVhZDpkZXZpY2VfY3JlZGVudGlhbHMgdXBkYXRlOmRldmljZV9jcmVkZW50aWFscyBkZWxldGU6ZGV2aWNlX2NyZWRlbnRpYWxzIGNyZWF0ZTpkZXZpY2VfY3JlZGVudGlhbHMgcmVhZDpydWxlcyB1cGRhdGU6cnVsZXMgZGVsZXRlOnJ1bGVzIGNyZWF0ZTpydWxlcyByZWFkOnJ1bGVzX2NvbmZpZ3MgdXBkYXRlOnJ1bGVzX2NvbmZpZ3MgZGVsZXRlOnJ1bGVzX2NvbmZpZ3MgcmVhZDpob29rcyB1cGRhdGU6aG9va3MgZGVsZXRlOmhvb2tzIGNyZWF0ZTpob29rcyByZWFkOmFjdGlvbnMgdXBkYXRlOmFjdGlvbnMgZGVsZXRlOmFjdGlvbnMgY3JlYXRlOmFjdGlvbnMgcmVhZDplbWFpbF9wcm92aWRlciB1cGRhdGU6ZW1haWxfcHJvdmlkZXIgZGVsZXRlOmVtYWlsX3Byb3ZpZGVyIGNyZWF0ZTplbWFpbF9wcm92aWRlciBibGFja2xpc3Q6dG9rZW5zIHJlYWQ6c3RhdHMgcmVhZDppbnNpZ2h0cyByZWFkOnRlbmFudF9zZXR0aW5ncyB1cGRhdGU6dGVuYW50X3NldHRpbmdzIHJlYWQ6bG9ncyByZWFkOmxvZ3NfdXNlcnMgcmVhZDpzaGllbGRzIGNyZWF0ZTpzaGllbGRzIHVwZGF0ZTpzaGllbGRzIGRlbGV0ZTpzaGllbGRzIHJlYWQ6YW5vbWFseV9ibG9ja3MgZGVsZXRlOmFub21hbHlfYmxvY2tzIHVwZGF0ZTp0cmlnZ2VycyByZWFkOnRyaWdnZXJzIHJlYWQ6Z3JhbnRzIGRlbGV0ZTpncmFudHMgcmVhZDpndWFyZGlhbl9mYWN0b3JzIHVwZGF0ZTpndWFyZGlhbl9mYWN0b3JzIHJlYWQ6Z3VhcmRpYW5fZW5yb2xsbWVudHMgZGVsZXRlOmd1YXJkaWFuX2Vucm9sbG1lbnRzIGNyZWF0ZTpndWFyZGlhbl9lbnJvbGxtZW50X3RpY2tldHMgcmVhZDp1c2VyX2lkcF90b2tlbnMgY3JlYXRlOnBhc3N3b3Jkc19jaGVja2luZ19qb2IgZGVsZXRlOnBhc3N3b3Jkc19jaGVja2luZ19qb2IgcmVhZDpjdXN0b21fZG9tYWlucyBkZWxldGU6Y3VzdG9tX2RvbWFpbnMgY3JlYXRlOmN1c3RvbV9kb21haW5zIHVwZGF0ZTpjdXN0b21fZG9tYWlucyByZWFkOmVtYWlsX3RlbXBsYXRlcyBjcmVhdGU6ZW1haWxfdGVtcGxhdGVzIHVwZGF0ZTplbWFpbF90ZW1wbGF0ZXMgcmVhZDptZmFfcG9saWNpZXMgdXBkYXRlOm1mYV9wb2xpY2llcyByZWFkOnJvbGVzIGNyZWF0ZTpyb2xlcyBkZWxldGU6cm9sZXMgdXBkYXRlOnJvbGVzIHJlYWQ6cHJvbXB0cyB1cGRhdGU6cHJvbXB0cyByZWFkOmJyYW5kaW5nIHVwZGF0ZTpicmFuZGluZyBkZWxldGU6YnJhbmRpbmcgcmVhZDpsb2dfc3RyZWFtcyBjcmVhdGU6bG9nX3N0cmVhbXMgZGVsZXRlOmxvZ19zdHJlYW1zIHVwZGF0ZTpsb2dfc3RyZWFtcyBjcmVhdGU6c2lnbmluZ19rZXlzIHJlYWQ6c2lnbmluZ19rZXlzIHVwZGF0ZTpzaWduaW5nX2tleXMgcmVhZDpsaW1pdHMgdXBkYXRlOmxpbWl0cyBjcmVhdGU6cm9sZV9tZW1iZXJzIHJlYWQ6cm9sZV9tZW1iZXJzIGRlbGV0ZTpyb2xlX21lbWJlcnMgcmVhZDplbnRpdGxlbWVudHMgcmVhZDphdHRhY2tfcHJvdGVjdGlvbiB1cGRhdGU6YXR0YWNrX3Byb3RlY3Rpb24gcmVhZDpvcmdhbml6YXRpb25zIHVwZGF0ZTpvcmdhbml6YXRpb25zIGNyZWF0ZTpvcmdhbml6YXRpb25zIGRlbGV0ZTpvcmdhbml6YXRpb25zIGNyZWF0ZTpvcmdhbml6YXRpb25fbWVtYmVycyByZWFkOm9yZ2FuaXphdGlvbl9tZW1iZXJzIGRlbGV0ZTpvcmdhbml6YXRpb25fbWVtYmVycyBjcmVhdGU6b3JnYW5pemF0aW9uX2Nvbm5lY3Rpb25zIHJlYWQ6b3JnYW5pemF0aW9uX2Nvbm5lY3Rpb25zIHVwZGF0ZTpvcmdhbml6YXRpb25fY29ubmVjdGlvbnMgZGVsZXRlOm9yZ2FuaXphdGlvbl9jb25uZWN0aW9ucyBjcmVhdGU6b3JnYW5pemF0aW9uX21lbWJlcl9yb2xlcyByZWFkOm9yZ2FuaXphdGlvbl9tZW1iZXJfcm9sZXMgZGVsZXRlOm9yZ2FuaXphdGlvbl9tZW1iZXJfcm9sZXMgY3JlYXRlOm9yZ2FuaXphdGlvbl9pbnZpdGF0aW9ucyByZWFkOm9yZ2FuaXphdGlvbl9pbnZpdGF0aW9ucyBkZWxldGU6b3JnYW5pemF0aW9uX2ludml0YXRpb25zIHJlYWQ6b3JnYW5pemF0aW9uc19zdW1tYXJ5IGNyZWF0ZTphY3Rpb25zX2xvZ19zZXNzaW9ucyBjcmVhdGU6YXV0aGVudGljYXRpb25fbWV0aG9kcyByZWFkOmF1dGhlbnRpY2F0aW9uX21ldGhvZHMgdXBkYXRlOmF1dGhlbnRpY2F0aW9uX21ldGhvZHMgZGVsZXRlOmF1dGhlbnRpY2F0aW9uX21ldGhvZHMiLCJndHkiOiJjbGllbnQtY3JlZGVudGlhbHMifQ.KbtQDTr_Gxp3RfmlaYOfQYo8HTb_QXUCqC_mgxDFqD-mLQtf3eTueutLnedI1FKDDQNvvShLpDTcTl1LAPysBSXjYX8DRTcXnM8D4kp6OHHHV4w8YBQ8aI-O4dXE8aiOSVRWnh_Dy6QpdROLYr4YkBSdMVi9ir4RH3UeVXuj3vtTdYfrqqssJVTcwwCkPeQhX1f1ZYCwhYYGAj-leuz0n1aHRy5nCMwofRhUvG-3kM4qyZOUrXvqkvdkxExPJ7RIX-ZANf2BVitpXzaKfTFet-pwSLbIM-7p99-3RMm5HKDPDDsSqv4_Jt8QgSRPq1rkAlrE7fDDN8MfYs6vV6ecaA");
                    var response = client.Execute(request);
                    if (response.IsSuccessful)
                    {
                        var json = JArray.Parse(response.Content);
                        return new OkObjectResult(json);
                    }
                    else
                    {
                        return new NotFoundResult();
                    }

                case "post-user-create":
                    if (WebVar == "NPO")
                    {
                        data = new NonProfitOrganization();
                        data = System.Text.Json.JsonSerializer.Deserialize<NonProfitOrganization>(req.Body);
                        MainManager.Instance.UsersM.SendUserToDB((NonProfitOrganization)data);
                        break;

                    }
                    else if (WebVar == "BC")
                    {
                        data = new BuisnessCompany();
                        data = System.Text.Json.JsonSerializer.Deserialize<BuisnessCompany>(req.Body);
                        MainManager.Instance.UsersM.SendUserToDB((BuisnessCompany)data);
                        break;
                    }
                    else
                    {
                        data = new SocialActivist();
                        data = System.Text.Json.JsonSerializer.Deserialize<SocialActivist>(req.Body);
                        MainManager.Instance.UsersM.SendUserToDB((SocialActivist)data);
                        break;
                    }

                case "get-user-info":
                    responseMessage = JsonConvert.SerializeObject(MainManager.Instance.UsersM.getUserInfoFromDB(WebVar, WebVar2));
                    return new OkObjectResult(responseMessage);

                case "get-pendding":
                    responseMessage = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.UsersM.getPenddingFromDB(WebVar));
                    return new OkObjectResult(responseMessage);

                case "get-pendding-list":
                    responseMessage = JsonConvert.SerializeObject(MainManager.Instance.UsersM.GetPenddingListFromDB());
                    return new OkObjectResult(responseMessage);

                case "get-my-donations":
                    responseMessage = JsonConvert.SerializeObject(MainManager.Instance.UsersM.GetSADonationsFromDB(WebVar));
                    return new OkObjectResult(responseMessage);

                case "get-campaigns":
                    responseMessage = JsonConvert.SerializeObject(MainManager.Instance.CampaignsM.GetCampaignsFromDB());
                    return new OkObjectResult(responseMessage);

                case "get-products":
                    responseMessage = JsonConvert.SerializeObject(MainManager.Instance.ProductsM.GetCampaignProductsFromDB());
                    return new OkObjectResult(responseMessage);


                case "get-orders":
                    responseMessage = JsonConvert.SerializeObject(MainManager.Instance.ProductsM.GetOrdersFromDB());
                    return new OkObjectResult(responseMessage);

                case "get-report":
                    responseMessage = JsonConvert.SerializeObject(MainManager.Instance.ReportsM.GetReportFromDB(WebVar));
                    return new OkObjectResult(responseMessage);

                case "post-approve-user":
                    MainManager.Instance.UsersM.ApproveUserInDB(WebVar);
                    break;

                case "post-order-shipped":
                    MainManager.Instance.ProductsM.ApproveOrderShippedInDB(WebVar);
                    break;

                case "post-user-message":
                    data = new UserMessage();
                    data = System.Text.Json.JsonSerializer.Deserialize<UserMessage>(req.Body);
                    MainManager.Instance.UsersM.SendUserMessageToDB((UserMessage)data);
                    break;

                case "post-new-campaign":
                    data = new Campaign();
                    data = System.Text.Json.JsonSerializer.Deserialize<Campaign>(req.Body);
                    MainManager.Instance.CampaignsM.SendNewCampaignToDB((Campaign)data, WebVar);
                    break;

                case "post-updated-campaign":
                    data = new Campaign();
                    data = System.Text.Json.JsonSerializer.Deserialize<Campaign>(req.Body);
                    MainManager.Instance.CampaignsM.SendUpdatedCampaignToDB((Campaign)data);
                    break;

                case "post-campaign-is-active":
                    MainManager.Instance.CampaignsM.ToggleCampaignIsActive(WebVar);
                    break;

                case "post-new-campaign-product":
                    data = new Product();
                    data = System.Text.Json.JsonSerializer.Deserialize<Product>(req.Body);
                    MainManager.Instance.ProductsM.SendNewCampaignProductToDB((Product)data, WebVar);
                    break;

                case "post-updated-campaign-product":
                    data = new Product();
                    data = System.Text.Json.JsonSerializer.Deserialize<Product>(req.Body);
                    MainManager.Instance.ProductsM.SendUpdatedCampaignProductToDB((Product)data);
                    break;

                case "post-donate-details":
                    data = new Order();
                    data = System.Text.Json.JsonSerializer.Deserialize<Order>(req.Body);
                    MainManager.Instance.ProductsM.SendDonateDetailsToDB((Order)data, int.Parse(WebVar));
                    break;
                case "post-sa-money-status":
                    data = System.Text.Json.JsonSerializer.Deserialize<decimal>(req.Body);
                    MainManager.Instance.UsersM.SendMoneyStatusToDB((decimal)data, int.Parse(WebVar));
                    break;

                case "get":
                    break;

                default:
                    break;

            }
            return null;
        }
    }
    //public static class Twitter
    //{
    //    [FunctionName("Twitter")]
    //    public static async Task<IActionResult> Run(
    //        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "twitter")] HttpRequest req,
    //        ILogger log)
    //    {
    //        var userClient = new TwitterClient("VZ5VkgFepx1My7TxmoWaXjCr6", "q8ufmEXGaow8YJq6cGXynFrDG4ilcvV1ee89PByGUEqLNoFPmy", "1614610924983259146-weskDlwL3A8IjV03n1Q902PLDhuMtA", "ZRwxVUQARSWcTpXhi9BGIimWrJV40Dmul9NPLdHMR1gXJ");

    //        var user = await userClient.Users.GetAuthenticatedUserAsync();
    //        Console.WriteLine(user);

    //        var tweet = await userClient.Tweets.PublishTweetAsync("#PromoIt Ba Rabbak");
    //        Console.WriteLine("You published the tweet : " + tweet);

    //        return new OkObjectResult(user);
    //    }
    //}

    //public static class Tweets
    //{
    //    private static string APIKey = "38GtVBOTriwJDED3x7tbPlQXv";
    //    private static string APIKeySecret = "I6cMBtct7YqHtvAx10BMHTtdhYdwexopE3HUTq8wxWIh90B8Ni";
    //    private static string AccessToken = "1607255581571649536-SHd7bffqugCkgrHPmj6Qc6PClVWIuG";
    //    private static string AccessTokenSecret = "JOyLBJSudXASgjlV8q8I1NhA2QiqHhha7nYLAggJPzeKW";
    //    public static async Task<ITweet> TweetAsync(TwitterClient user, string tweetText)
    //    {
    //        if (user == null)
    //            return null;

    //        try
    //        {
    //            return await user.Tweets.PublishTweetAsync(tweetText);
    //        }
    //        catch (Exception)
    //        {
    //            throw;
    //        }
    //    }

        //[FunctionName("Tweets")]
        //public static async Task<IActionResult> Run(
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Tweets/{action}/{type}/{user}/{hashtag}")] HttpRequest req,
        //    string action, string type, string user, string hashtag, ILogger log)
        //{
        //    //in case of post--> type = activistTwitterHandle, user = companyTwitterHandle, hashtag = productId

        //    log.LogInformation("C# HTTP trigger function processed a request.");
        //    string responseBody = "";

        //    switch (action)
        //    {
        //        case "Post":
        //            TwitterClient userClient = new TwitterClient(APIKey, APIKeySecret, AccessToken, AccessTokenSecret);
        //            // request the user's information from Twitter API
        //            //var user1 = await userClient.Users.GetAuthenticatedUserAsync();
        //            // publish a tweet
        //            string tweetText = $"Congratulations! @{type} just bought a product(id:{hashtag}) from @{user}, in PromoIt you can promote the society and also earn points and buy cool products.";
        //            var tweet = await TweetAsync(userClient, tweetText);
        //            //var tweet = await userClient.Tweets.PublishTweetAsync("Hello tweetinvi world!");

        //            /*
        //             HttpClient client1 = new HttpClient();
        //             HttpRequestMessage request1 = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, "https://api.twitter.com/2/tweets");
        //             request1.Headers.Add("Authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAKthlAEAAAAAS%2BZoFBRCRqbdy05wokFoXm24lgY%3DknwW3XtvbroPk48HJhUdI46AbUx7cB3CIxqICOxGx8WzsfuT56");
        //             //string tweetText = $"Congratulations! @{type} just bought a product from @{user}, in PromoIt you can promote the society and also earn points and buy cool products.";
        //             request1.Content = new StringContent("{\"status\": \"Hello, world!\"}");
        //             request1.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");                    
        //             HttpResponseMessage response1 = await client1.SendAsync(request1);
        //             response1.EnsureSuccessStatusCode();
        //             responseBody = await response1.Content.ReadAsStringAsync();
        //             */
        //            /*HttpClient client1 = new HttpClient();
        //            HttpRequestMessage request1 = new HttpRequestMessage(HttpMethod.Post, "https://api.twitter.com/oauth2/token");
        //            request1.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes("yauvoru5WnQtkLtC3qkkbcANj:wO5Dic1kLWJM0itg0FjKqEl9sR4CaOxkNUKDJgF7oMTCt0AI1p")));
        //            request1.Content = new StringContent("grant_type=client_credentials");
        //            request1.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
        //            HttpResponseMessage response1 = await client1.SendAsync(request1);
        //            response1.EnsureSuccessStatusCode();
        //            responseBody = await response1.Content.ReadAsStringAsync();*/
        //            break;

        //        case "Get":
        //            if (type == "retweet")
        //            {
        //                HttpClient client = new HttpClient();
        //                HttpRequestMessage request = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, $"https://api.twitter.com/2/tweets/search/recent?query=retweets_of%3A{user}%20%22{hashtag}%22%20has%3Ahashtags%20has%3Alinks%20is%3Aretweet");
        //                request.Headers.Add("Authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAKthlAEAAAAAS%2BZoFBRCRqbdy05wokFoXm24lgY%3DknwW3XtvbroPk48HJhUdI46AbUx7cB3CIxqICOxGx8WzsfuT56");
        //                HttpResponseMessage response = await client.SendAsync(request);
        //                response.EnsureSuccessStatusCode();
        //                responseBody = await response.Content.ReadAsStringAsync();
        //            }
        //            else if (type == "tweet")
        //            {
        //                HttpClient client = new HttpClient();
        //                HttpRequestMessage request = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, $"https://api.twitter.com/2/tweets/search/recent?query=from%3A{user}%20{hashtag}%20has%3Ahashtags%20has%3Alinks%20-is%3Aretweet");
        //                request.Headers.Add("Authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAKthlAEAAAAAS%2BZoFBRCRqbdy05wokFoXm24lgY%3DknwW3XtvbroPk48HJhUdI46AbUx7cB3CIxqICOxGx8WzsfuT56");
        //                HttpResponseMessage response = await client.SendAsync(request);
        //                response.EnsureSuccessStatusCode();
        //                responseBody = await response.Content.ReadAsStringAsync();
        //            }
        //            break;
        //    }

        //    return new OkObjectResult(responseBody);
        //}
    //}
}


