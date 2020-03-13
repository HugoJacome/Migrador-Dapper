using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigradorDapper.Models.CardController
{

    public class CardsAccounts
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public bool IsPrincipal { get; set; }
    }
}
