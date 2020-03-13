using MigradorDapper.Models.CardController;
using MigradorDapper.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace MigradorDapper
{
    public class Migration
    {
        private static int institutionID = Convert.ToInt32(ConfigurationManager.AppSettings["InstitutionID"]);
        private static string connStringOld = ConfigurationManager.AppSettings["connStringOld"];
        private static string connStringCardDB = ConfigurationManager.AppSettings["connStringCardDB"];
        private static string connStringPhiAdmin = ConfigurationManager.AppSettings["connStringPhiAdmin"];
        CardControllerRepository ccRepo = new CardControllerRepository(connStringOld);
        AdmTDDsRepository admTddsRepo = new AdmTDDsRepository(connStringCardDB);
        PhiAdminRepositrory phiAdminRepo = new PhiAdminRepositrory(connStringPhiAdmin);

        public async Task<int> MigraAgencias()
        {
            List<Branch> branches = new List<Branch>();
            var agencias = await ccRepo.GetBranchs();

            foreach (var ag in agencias)
            {
                Branch branch = new Branch
                {
                    InstitutionId = institutionID,
                    State = (ag.AGE_ESTADO == "A") ? true : false,
                    Zone = ag.AGE_ZONA,
                    Name = ag.AGE_DESCRIPCION,
                    Address = ag.AGE_UBICACION,
                    CoreIdentifier = ag.AGE_CODIGO.ToString()
                };
                branches.Add(branch);
            }
            try
            {
                var migrated = await phiAdminRepo.MigrateBranchs(branches);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Agencia" + e.Message);
                throw;
            }
            return branches.Count();
        }
        
        public async Task<int> MigraOrdenes()
        {
            List<Orders> orders = new List<Orders>();
            var ordenes = await ccRepo.GetOrders();
            foreach (var orden in ordenes)
            {
                Orders order = new Orders
                {
                    ProfileId = 1,
                    PrefixId = 1,
                    State = 3,
                    InitialSequential = 0,
                    Quantity = orden.ORD_TOTAL,
                    CreationDate = orden.ORD_FECHA_CREACION,
                    ClosingDate = orden.ORD_FECHA_CIERRE,
                    UserName = "MIGRACION"
                };
                orders.Add(order);
            }
            try
            {
                var migrated = await admTddsRepo.MigrateOrders(orders);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Ordenes" + e.Message);
                throw;
            }
            return orders.Count();
        }
        public async Task<int> MigrarClientes29()
        {
            List<Clients> clients = new List<Clients>();
            int[] states = new int[] { 1, 2, 3, 5, 6, 11, 12, 13 };
            var clientes = await ccRepo.GetClients(states);
            var branchId = await phiAdminRepo.GetBranchById(institutionID);
            foreach (var cli in clientes)
            {
                Clients client = new Clients()
                {
                    Name = cli.CLI_NOMBRE,
                    Identification = cli.CLI_IDENTIFICACION,
                    AgencyId = branchId,
                    IdentityType = (cli.CLI_IDENTIFICACION.Length == 10) ? "C" : "R"
                };
                clients.Add(client);
            }
            try
            {
                var migrated = await admTddsRepo.MigrateClients(clients);
            }
            catch (Exception e)
            {
                Console.WriteLine("error cliente: " + e.Message);
                throw;
            }
            return clients.Count();
        }
        public async Task<int> MigraCuentasTarjetas29()
        {
            CardsAccounts account = new CardsAccounts();
            List<CardsAccounts> accounts = new List<CardsAccounts>();

            var tarjetas = await ccRepo.GetCards();
            var cuentasTarjetas = await ccRepo.GetAccountCards();

            var accountsM = tarjetas
                .Join(cuentasTarjetas,
                    t => Convert.ToInt32(t.CardNumber),
                    tc => tc.TAR_ID,
                    (t, tar) => new { t, tar })
                    .Select(tarjeta => new
                    {
                        CardId = tarjeta.t.Id,
                        AccountNumber = tarjeta.tar.CTA_NUMERO
                    }).Take(100).ToList();

            foreach (var acc in accountsM)
            {
                account = new CardsAccounts
                {
                    CardId = acc.CardId,
                    AccountNumber = acc.AccountNumber,
                    AccountType = "AHORROS",
                    IsPrincipal = true
                };
                accounts.Add(account);
            }

            try
            {
                var migrated = await admTddsRepo.MigrateCardsAccounts(accounts);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error cuentas tarjetas: " + e.Message);
                throw;
            }
            return accounts.Count();
        }

        public async Task<int> MigraTarjetasOrden29()
        {
            int[] states = new int[] { 1, 2, 3, 5, 6, 11, 12, 13 };
            Cards cardNew = new Cards();
            List<Cards> cards = new List<Cards>();
            var branchId = await phiAdminRepo.GetBranchById(institutionID);
            var tarjetas = await ccRepo.GetOrderCards(states);
            var clientes = await ccRepo.GetClientsById();

            var cardsOld = tarjetas
                   .Join(clientes,
                   t => t.CLI_IDENTIFICACION,
                   c => c.Identification,
                   (t, c) => new { t, c })
                   .Select(tarjeta => new
                   {
                       Id = tarjeta.t.TAR_ID,
                       OrderId = tarjeta.t.ORD_CODIGO,
                       State = tarjeta.t.EST_TAR_CODIGO,
                       ExpirationDate = tarjeta.t.TAR_FECHA_EXPIRACION,
                       LastMovDate = tarjeta.t.TAR_FECHA_ULTIMO_MOV,
                       EmissionDate = tarjeta.t.TAR_FECHA_SOLICITUD,
                       PINLastChangeDate = tarjeta.t.TAR_FECHA_ULT_CAMBIOPIN,
                       ClientID = tarjeta.c.Id,
                       ClientIdentification = tarjeta.t.CLI_IDENTIFICACION,
                       OFFSET = tarjeta.t.TAR_OFFSET,
                       ATC_EMV = tarjeta.t.TAR_EMV_SEC,
                       PrintedName = tarjeta.t.TAR_NOMBRE_IMPRESO,
                       BranchId = tarjeta.t.AGE_CODIGO,
                       MaskedNumber = tarjeta.t.TAR_NUMERO,
                       HASH = tarjeta.t.TAR_HASH,
                       CardNumber = tarjeta.t.TAR_NUMERO,
                       CardDeliveryDate = tarjeta.t.TAR_FECHA_ENTREGA,
                       CardAccount = tarjeta.t.CTA_NUMERO,
                   })
                   .ToList();

            foreach (var card in cardsOld)
            {
                int state = (card.State == 12 || card.State == 1) ? ((card.State == 12) ? 1 : 12) : card.State;
                cardNew = new Cards()
                {
                    Id = Convert.ToInt32(card.Id),
                    ProfileId = 1,
                    OrderId = card.OrderId,
                    BatchId = 1,
                    State = state,
                    ExpirationDate = card.ExpirationDate,
                    LastMovDate = card.LastMovDate,
                    EmissionDate = card.EmissionDate,
                    PINLastChangeDate = card.PINLastChangeDate,
                    ClientId = card.ClientID,
                    CashQuota = 300,
                    PurchaseQuota = 0,
                    OFFSET = card.OFFSET,
                    Sequential = "",
                    PINAttempts = 0,
                    ATC_EMV = card.ATC_EMV,
                    PrintedName = card.PrintedName,
                    DeliveryBranchId = card.BranchId,
                    ModelId = 1,
                    MaskedNumber = card.MaskedNumber,
                    HASH = card.HASH,
                    Seed = "",
                    ChangePIN = false,
                    LastCashDate = card.LastMovDate,
                    LastPurchaseDate = card.LastMovDate,
                    CardDeliveryDate = card.CardDeliveryDate,
                    CardActivationDate = card.EmissionDate,
                    CashTransPerDay = 0,
                    RequestBranchId = branchId,
                    CardNumber = card.CardNumber
                };
            }
            try
            {
                var migrated = await admTddsRepo.MigrateTarjetasOrden29(cards);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error tarjetas orden: " + e.Message);
                throw;
            }
            return cards.Count();
        }
    }
}