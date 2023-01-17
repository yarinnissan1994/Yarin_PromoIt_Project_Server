using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.model
{
    public class Product
    {
        public int? Code { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public decimal Price { set; get; }
        public int UnitsInStock { set; get; }
        public int BCCode { set; get; }
        public int CampaignCode { set; get; }
        public string MyImage { get; set; }
    }
}
