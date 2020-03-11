using MigradorDapper.Models;
using MigradorDapper.Models.AdminTDDs;
using System.Linq;
using System.Threading.Tasks;

namespace MigradorDapper.Repositories
{
    public class CardControllerRepository : BaseRepository
    {
        public CardControllerRepository(string connectionString) : base(connectionString)
        {
        }
        public async Task<Agencia[]> GetBranchs()
        {
            const string query = @"SELECT 
                                    AGE_CODIGO, AGE_DESCRIPCION,
                                    AGE_UBICACION, AGE_ZONA, AGE_ESTADO
                                    FROM AGENCIA";
            return (await EnumerableQueryAsync<Agencia>(query, new { })).ToArray();
        }
    }
}
