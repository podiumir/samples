using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FanapPlus.Torange.Samples.Web.Infrastructures;
using Newtonsoft.Json;

namespace FanapPlus.Torange.Samples.Web.Clients.Pod.Models
{
    public class Rule
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("creationDate")]
        [JsonConverter(typeof(MicrosecondEpochConverter))]
        public DateTime CreationDate { get; set; }

        [JsonProperty("startDate")]
        [JsonConverter(typeof(MicrosecondEpochConverter))]
        public DateTime StartDate { get; set; }

        [JsonProperty("expireDate")]
        [JsonConverter(typeof(MicrosecondEpochConverter))]
        public DateTime ExpireDate { get; set; }

        [JsonProperty("maxAmount")]
        public int MaxAmount { get; set; }

        [JsonProperty("maxCount")]
        public int MaxCount { get; set; }

        [JsonProperty("typeCode")]
        public string TypeCode { get; set; }

        [JsonProperty("usageCount")]
        public int UsageCount { get; set; }

        [JsonProperty("usageAmount")]
        public int UsageAmount { get; set; }

        [JsonProperty("wallet")]
        public string Wallet { get; set; }

        [JsonProperty("expired")]
        public bool Expired { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}
