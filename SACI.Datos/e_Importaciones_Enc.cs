using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SACI.Datos
{
    public class e_Importaciones_Enc
    {
        public string documento { get; set; }
        public DateTime fecha { get; set; }
        public string  aduana { get; set; }
        public string patente { get; set; }
        public string cod_proveedor { get; set; }
        public string cve_pedimento { get; set; }
        public decimal tc { get; set; }
        public decimal aduana_imp { get; set; }
        public decimal val_dls { get; set; }
        public string descarga { get; set; }
        public string carga_m { get; set; }
        public string div_alm { get; set; }

    }
}
