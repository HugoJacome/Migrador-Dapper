using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigradorDapper.Models.CardController
{
    public class Orders
    {
        public int Id { get; set; }
        public int? ProfileId { get; set; }
        public int? PrefixId { get; set; }
        public int? State { get; set; }
        public int? InitialSequential { get; set; }
        public int? Quantity { get; set; }
        public System.DateTime CreationDate { get; set; }
        public DateTime? NotifyDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string UserName { get; set; }
        public bool? AutoGeneration { get; set; }
    }
}
