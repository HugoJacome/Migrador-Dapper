using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigradorDapper
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Migration migration = new Migration();

            //await migration.MigraAgencias();
            //await migration.MigraOrdenes();
            //await migration.MigrarClientes29();
            //await migration.MigraTarjetasOrden29();
            await migration.MigraCuentasTarjetas29();
        }
    }
}
