using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.model
{
    public class NonProfitOrganization
    {
        public int Code { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        public string WebsiteURL { set; get; }
        public byte[] MyImage { get; set; }
    }
}
