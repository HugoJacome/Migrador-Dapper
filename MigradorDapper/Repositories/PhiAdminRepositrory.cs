using MigradorDapper.Models;
using MigradorDapper.Models.CardController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigradorDapper.Repositories
{
    public class PhiAdminRepositrory : BaseRepository
    {
        public PhiAdminRepositrory(string connectionString) : base(connectionString)
        {
        }
        public async Task<int> MigrateBranchs(List<Branch> branches)
        {
            const string query = @"INSERT INTO [dbo].[Branches]
                                               ([InstitutionId]
                                               ,[State]
                                               ,[City]
                                               ,[Province]
                                               ,[Location]
                                               ,[Zone]
                                               ,[Name]
                                               ,[Address]
                                               ,[CoreIdentifier]
                                               ,[Identifiers])
                                         VALUES
                                               (@InstitutionId
                                               ,@State
                                               ,@City
                                               ,@Province
                                               ,@Location
                                               ,@Zone
                                               ,@Name
                                               ,@Address
                                               ,@CoreIdentifier
                                               ,@Identifiers)";
            return await InsertQueryAsync<int>(query, branches);
        }
        public async Task<int> GetBranchById(int branchId)
        {
            const string query = @"SELECT top 1
                                    INSTITUTIONID
                                    FROM BRANCHES
                                    WHERE INSTITUTIONID = @BRANCHID";
            return (await QuerySingleOrDefaultAsync<int>(query, new { BRANCHID = branchId }));
        }
        
    }
}