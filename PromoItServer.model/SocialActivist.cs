using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.model
{
    public class SocialActivist
    {
        public int Code { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        public string Address { set; get; }
        public string PhoneNumber { set; get; }
        public decimal MoneyStatus { set; get; }
        public string MyImage { get; set; }
        public string TwitterName { set; get; }
    }
}
