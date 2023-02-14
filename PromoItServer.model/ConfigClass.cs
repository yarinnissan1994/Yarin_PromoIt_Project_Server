using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.model
{
    public class ConfigClass
    {
        public int Code { set; get; }
        public string Auth0BearerCode { set; get; }
        public string TwitterBearerCode { set; get; }
        public string ConsumerKey { set; get; }
        public string ConsumerKeySecret { set; get; }
        public string AccessKey { set; get; }
        public string AccessKeySecret { set; get; }
    }
}
