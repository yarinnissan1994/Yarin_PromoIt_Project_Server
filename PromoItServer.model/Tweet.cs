using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.model
{
    public class Tweet
    {
        public int Code { set; get; }
        public int SACode { set; get; }
        public int CampaignCode { set; get; }
        public string HashTag { set; get; }
        public string LandingPageURL { set; get; }
        public string TweetContent { set; get; }
        public DateTime TweetTime { set; get; }
    }
}
