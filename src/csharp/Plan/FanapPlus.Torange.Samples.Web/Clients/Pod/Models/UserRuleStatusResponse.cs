using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FanapPlus.Torange.Samples.Web.Clients.Pod.Models
{
    public class GetUserRuleStatusResponse
    {
        [JsonProperty("rule")]
        public Rule Rule { get; set; }

        [JsonProperty("validUntil")]
        public long ValidUntil { get; set; }

        [JsonProperty("remainingAmount")]
        public int RemainingAmount { get; set; }

        [JsonProperty("remainingCount")]
        public int RemainingCount { get; set; }
    }
}
