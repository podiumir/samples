using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FanapPlus.Torange.Samples.Web.Clients.Pod.Models
{
    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ssoId")]
        public string SsoId { get; set; }

        [JsonProperty("ssoIssuerCode")]
        public int SsoIssuerCode { get; set; }

        [JsonProperty("profileImage")]
        public string ProfileImage { get; set; }
    }
}
