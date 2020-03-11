using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigradorDapper.Models.AdminTDDs
{
    public class Orden
    {
        public int ORD_CODIGO { get; set; }
        public DateTime ORD_FECHA_CREACION { get; set; }
        public DateTime? ORD_FECHA_CIERRE { get; set; }
        public int ORD_TOTAL { get; set; }
        public string ORD_ESTADO { get; set; }
        public string ORD_TIPO { get; set; }
        public string INST_CODIGO { get; set; }
    }
}
