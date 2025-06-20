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
    public partial class RNSMS_Token
    {

        [JsonProperty(PropertyName = "Authorization")]
        public String Authorization { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public String TokenType { get; set; }

        [JsonIgnore]
        public Boolean TieneError { get; set; }


    }
}
