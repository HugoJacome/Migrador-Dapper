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

        public async Task<int> MigrateOrders(List<Orders> orders)
        {
            const string query = @"INSERT INTO [dbo].[Orders]
                                               ([ProfileId]
                                               ,[PrefixId]
                                               ,[State]
                                               ,[InitialSequential]
                                               ,[Quantity]
                                               ,[CreationDate]
                                               ,[NotifyDate]
                                               ,[ClosingDate]
                                               ,[UserName]
                                               ,[AutoGeneration])
                                         VALUES
                                               (@ProfileId
                                               ,@PrefixId
                                               ,@State
                                               ,@InitialSequential
                                               ,@Quantity
                                               ,@CreationDate
                                               ,@NotifyDate
                                               ,@ClosingDate
                                               ,@UserName
                                               ,@AutoGeneration)";
            return await InsertQueryAsync<int>(query, orders);
        }

        public async Task<int> MigrateClients(List<Clients> clients)
        {
            const string query = @"INSERT INTO [dbo].[Clients]
                                               ([Identification]
                                               ,[Name]
                                               ,[AgencyId])
                                         VALUES
                                               (@Identification
                                               ,@Name
                                               ,@AgencyId)";
            return await InsertQueryAsync<int>(query, clients);
        }

        public async Task<int> MigrateTarjetasOrden29(List<Cards> cards)
        {
            const string query = @"INSERT INTO [dbo].[Cards]
                                               ([ProfileId]
                                               ,[OrderId]
                                               ,[BatchId]
                                               ,[State]
                                               ,[ExpirationDate]
                                               ,[LastMovDate]
                                               ,[EmissionDate]
                                               ,[PINLastChangeDate]
                                               ,[ClientId]
                                               ,[CashQuota]
                                               ,[PurchaseQuota]
                                               ,[OFFSET]
                                               ,[Sequential]
                                               ,[PINAttempts]
                                               ,[ATC_EMV]
                                               ,[PrintedName]
                                               ,[DeliveryBranchId]
                                               ,[ModelId]
                                               ,[MaskedNumber]
                                               ,[HASH]
                                               ,[Seed]
                                               ,[ChangePIN]
                                               ,[LastCashDate]
                                               ,[LastPurchaseDate]
                                               ,[CardDeliveryDate]
                                               ,[CardActivationDate]
                                               ,[CashTransPerDay]
                                               ,[RequestBranchId]
                                               ,[CardNumber]
                                               ,[LastMaintenanceFeeChargeDate]
                                               ,[LastTransactions])
                                         VALUES
                                               (@ProfileId
                                               ,@OrderId
                                               ,@BatchId
                                               ,@State
                                               ,@ExpirationDate
                                               ,@LastMovDate
                                               ,@EmissionDate
                                               ,@PINLastChangeDate
                                               ,@ClientId
                                               ,@CashQuota
                                               ,@PurchaseQuota
                                               ,@OFFSET
                                               ,@Sequential
                                               ,@PINAttempts
                                               ,@ATC_EMV
                                               ,@PrintedName
                                               ,@DeliveryBranchId
                                               ,@ModelId
                                               ,@MaskedNumber
                                               ,@HASH
                                               ,@Seed
                                               ,@ChangePIN
                                               ,@LastCashDate
                                               ,@LastPurchaseDate
                                               ,@CardDeliveryDate
                                               ,@CardActivationDate
                                               ,@CashTransPerDay
                                               ,@RequestBranchId
                                               ,@CardNumber
                                               ,@LastMaintenanceFeeChargeDate
                                               ,@LastTransactions)";
            return await InsertQueryAsync<int>(query, cards);
        }

        public async Task<int> MigrateCardsAccounts(List<CardsAccounts> clients)
        {
            const string query = @"INSERT INTO [dbo].[CardsAccounts]
                                               ([CardId]
                                               ,[AccountNumber]
                                               ,[AccountType]
                                               ,[IsPrincipal])
                                         VALUES
                                               (@CardId
                                               ,@AccountNumber
                                               ,@AccountType
                                               ,@IsPrincipal)";
            return await InsertQueryAsync<int>(query, clients);
        }
    }
}