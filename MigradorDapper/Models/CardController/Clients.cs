using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigradorDapper.Models.CardController
{
    public class Clients
    {
        public int Id { get; set; }
        public string Identification { get; set; }
        public string Name { get; set; }
        public int AgencyId { get; set; }
        public string IdentityType { get; set; }
    }
}
