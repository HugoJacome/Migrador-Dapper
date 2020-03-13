using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigradorDapper.Models.CardController
{
    public class Cards
    {
        public int Id { get; set; }
        public int? ProfileId { get; set; }
        public int OrderId { get; set; }
        public int? BatchId { get; set; }
        public int State { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? LastMovDate { get; set; }
        public DateTime? EmissionDate { get; set; }
        public DateTime? PINLastChangeDate { get; set; }
        public int? ClientId { get; set; }
        public decimal? CashQuota { get; set; }
        public decimal? PurchaseQuota { get; set; }
        public string OFFSET { get; set; }
        public string Sequential { get; set; }
        public int? PINAttempts { get; set; }
        public int? ATC_EMV { get; set; }
        public string PrintedName { get; set; }
        public int? DeliveryBranchId { get; set; }
        public int? ModelId { get; set; }
        public string MaskedNumber { get; set; }
        public string HASH { get; set; }
        public string Seed { get; set; }
        public bool? ChangePIN { get; set; }
        public DateTime? LastCashDate { get; set; }
        public DateTime? LastPurchaseDate { get; set; }
        public DateTime? CardDeliveryDate { get; set; }
        public DateTime? CardActivationDate { get; set; }
        public int? CashTransPerDay { get; set; }
        public int? RequestBranchId { get; set; }
        public string CardNumber { get; set; }
        public DateTime? LastMaintenanceFeeChargeDate { get; set; }
        public string LastTransactions { get; set; }
    }
}
