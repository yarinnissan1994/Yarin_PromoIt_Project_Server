using PromoItServer.DataSql;
using PromoItServer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities
{
    public class RegisterApplicationsManager
    {
        public bool? getPenddingFromDB(string email)
        {
            RegisterApplicationsQ registerApplicationsQ = new RegisterApplicationsQ();
            return registerApplicationsQ.getPenddingQuery(email);
        }
    }
}
