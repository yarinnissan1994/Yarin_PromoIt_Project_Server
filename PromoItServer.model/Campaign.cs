using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.model
{
    public class Campaign
    {
        public int Code { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        public string LandingPageURL { set; get; }
        public string HashTag { set; get; }
        public int NPOCode { set; get; }
        public byte[] MyImage { get; set; }
        public bool IsActive { set; get; }
    }
}
