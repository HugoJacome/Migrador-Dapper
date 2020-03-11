using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigradorDapper.Models.CardController
{
    public class Branch
    {
        public int InstitutionId { get; set; }
        public bool State { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Location { get; set; }
        public string Zone { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CoreIdentifier { get; set; }
        public string Identifiers { get; set; }
    }
}
