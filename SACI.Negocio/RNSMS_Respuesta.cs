using SACI.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SACI.Negocio
{
    public partial class RNSMS_Respuesta
    {
        
        [JsonProperty(PropertyName = "code")]
        public String code { get; set; }

        [JsonProperty(PropertyName = "mailingId")]
        public String mailingId { get; set; }

        [JsonProperty(PropertyName = "result")]
        public String result { get; set; }

        [JsonProperty(PropertyName = "scheduledAt")]
        public String scheduledAt { get; set; }

        [JsonProperty(PropertyName = "hint")]
        public String hint { get; set; }


        [JsonIgnore]
        public Boolean TieneError { get; set; }

        [JsonProperty(PropertyName = "message")]
        public String message { get; set; }

        [JsonIgnore]
        public String Response { get; set; }

    }
}
