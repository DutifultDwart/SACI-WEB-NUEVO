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
    public partial class RNSMS
    {

        [JsonProperty(PropertyName = "apiKey")]
        public String apiKey { get; set; }

        [JsonProperty(PropertyName = "country")]
        public String country { get; set; }

        [JsonProperty(PropertyName = "dial")]
        public String dial { get; set; }

        [JsonProperty(PropertyName = "message")]
        public String message { get; set; }

        [JsonProperty(PropertyName = "msisdns")]
        public String msisdns { get; set; }

        [JsonProperty(PropertyName = "tag")]
        public String tag { get; set; }

        

    }
}
