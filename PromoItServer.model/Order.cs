using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.model
{
    public class Order
    {
        public int Code { set; get; }
        public int SACode { set; get; }
        public int BCCode { set; get; }
        public int CampaignCode { set; get; }
        public int ProductCode { set; get; }
        public DateTime dateTime { set; get; }
        public bool IsSent { set; get; }
    }
}
