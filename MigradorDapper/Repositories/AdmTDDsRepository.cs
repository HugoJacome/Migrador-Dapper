using MigradorDapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigradorDapper.Repositories
{
    public class AdmTDDsRepository: BaseRepository
    {
        public AdmTDDsRepository(string connectionString) : base(connectionString)
        {
        }
    }
}
