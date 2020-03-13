using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigradorDapper.Models.AdminTDDs
{
    public class Tarjeta
    {
        public int TAR_ID { get; set; }
        public int ORD_CODIGO { get; set; }
        public int EST_TAR_CODIGO { get; set; }
        public DateTime? TAR_FECHA_EXPIRACION { get; set; }
        public DateTime? TAR_FECHA_ULTIMO_MOV { get; set; }
        public DateTime? TAR_FECHA_SOLICITUD { get; set; }
        public DateTime? TAR_FECHA_ULT_CAMBIOPIN { get; set; }
        //public int ID { get; set; }
        public string CLI_IDENTIFICACION { get; set; }
        public string TAR_OFFSET { get; set; }
        public int TAR_EMV_SEC { get; set; }
        public string TAR_NOMBRE_IMPRESO { get; set; }
        public int AGE_CODIGO { get; set; }
        public string TAR_NUMERO { get; set; }
        public string TAR_HASH { get; set; }
        public DateTime TAR_FECHA_ENTREGA { get; set; }
        public string CTA_NUMERO { get; set; }
    }
}
