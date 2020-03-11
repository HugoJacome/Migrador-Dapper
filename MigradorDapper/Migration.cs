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
    }
}
