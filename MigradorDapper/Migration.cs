using Dapper;
using MigradorDapper.Models.CardController;
using MigradorDapper.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
                var migrated = await phiAdminRepo.MigrateOrders(orders);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Agencia" + e.Message);
                throw;
            }
            return orders.Count();
        }
        public async Task<int> MigrarClientes()
        {
            Clients client = new Clients();
            List<Clients> clients = new List<Clients>();
            int[] states = new int[] { 1, 2, 3, 5, 6, 11, 12, 13 };
            var clientes = await ccRepo.GetClients(states);

            foreach (var cli in clientes)
            {
                client = new Clients()
                {
                    Name = cli.CLI_NOMBRE,
                    Identification = cli.CLI_IDENTIFICACION,
                    AgencyId = cli.AGE_CODIGO,
                    IdentityType = (cli.CLI_IDENTIFICACION.Length == 10) ? "C" : "R"
                };
                clients.Add(client);
            }
            try
            {
                var migrated = await phiAdminRepo.MigrateClients(clients);
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
            int[] states = new int[] { 1, 2, 3, 5, 6, 11, 12, 13 };
            Cards cardNew = new Cards();
            List<Cards> cards = new List<Cards>();

            var tarjetas = await ccRepo.GetOrderCards(states);
            var clientes = await ccRepo.GetClientsById();

       
       


            foreach (var card in tarjetas)
            {
                int state = (card.EST_TAR_CODIGO == 12 || card.EST_TAR_CODIGO == 1) ? ((card.EST_TAR_CODIGO == 12) ? 1 : 12) : card.EST_TAR_CODIGO;
                cardNew = new Cards()
                {
                   // Id = card.ID,
                    ProfileId = 1,
                    OrderId = card.ORD_CODIGO,
                    BatchId = 1,
                    State = state,
                };
            }
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine("error tarjeta cuenta: " + e);
                throw;
            }
            return cards.Count();
        }
    }
}