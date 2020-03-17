using MigradorDapper.Models;
using MigradorDapper.Models.AdminTDDs;
using MigradorDapper.Models.CardController;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MigradorDapper.Repositories
{
    public class CardControllerRepository : BaseRepository
    {
        public CardControllerRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<Orden[]> GetOrders()
        {
            const string query = @"SELECT
                                    ORD_CODIGO, ORD_FECHA_CREACION, ORD_FECHA_CIERRE, 
                                    ORD_TOTAL, ORD_ESTADO, ORD_TIPO, INST_CODIGO
                                    FROM ORDEN";
            return (await EnumerableQueryAsync<Orden>(query, new { })).ToArray();
        }
       

        public async Task<Agencia[]> GetBranchs()
        {
            const string query = @"SELECT
                                    AGE_CODIGO, AGE_DESCRIPCION,
                                    AGE_UBICACION, AGE_ZONA, AGE_ESTADO
                                    FROM AGENCIA";
            return (await EnumerableQueryAsync<Agencia>(query, new { })).ToArray();
        }
        
     
        public async Task<Cliente[]> GetClients(int[] states)
        {
            const string query = @"SELECT 
                                    C.CLI_IDENTIFICACION, C.CLI_NOMBRE, T.AGE_CODIGO
                                    FROM TARJETA_CUENTA TC 
                                    JOIN CLIENTE C 
                                    ON C.CLI_CODIGO = TC.CLI_CODIGO
                                    JOIN TARJETA T
                                    ON TC.TAR_ID = T.TAR_ID
                                    WHERE T.EST_TAR_CODIGO IN  @STATES";
            return (await EnumerableQueryAsync<Cliente>(query, new { STATES = states })).ToArray();
        }
                
        public async Task<Tarjeta[]> GetOrderCards(int[] states)
        {
            const string query = @"SELECT
                                    T.TAR_ID, T.ORD_CODIGO, T.EST_TAR_CODIGO, T.TAR_FECHA_EXPIRACION,
                                    T.TAR_FECHA_ULTIMO_MOV, T.TAR_FECHA_SOLICITUD, T.TAR_FECHA_ULT_CAMBIOPIN,                                    
                                    C.CLI_IDENTIFICACION, T.TAR_OFFSET, T.TAR_EMV_SEC, T.TAR_NOMBRE_IMPRESO,
                                    T.AGE_CODIGO, T.TAR_NUMERO, T.TAR_HASH, T.TAR_NUMERO, T.TAR_FECHA_ENTREGA,
                                    TC.CTA_NUMERO
                                    FROM TARJETA_CUENTA TC
                                    JOIN TARJETA T 
                                    ON TC.TAR_ID = T.TAR_ID
                                    JOIN CLIENTE C 
                                    ON C.CLI_CODIGO = TC.CLI_CODIGO
                                    WHERE T.EST_TAR_CODIGO IN @STATES";
            return (await EnumerableQueryAsync<Tarjeta>(query, new { STATES = states })).ToArray();
        }

        
        public async Task<Tarjeta[]> GetAccountCards()
        {
            const string query = @"SELECT TC.CTA_NUMERO, T.TAR_NUMERO FROM TARJETA_CUENTA TC
                                    JOIN TARJETA T ON T.TAR_ID = TC.TAR_ID";
            return (await EnumerableQueryAsync<Tarjeta>(query, new {})).ToArray();
        }
    }
}
