using System;
using FanapPlus.Torange.Samples.Web.Infrastructures;
using Newtonsoft.Json;

namespace FanapPlus.Torange.Samples.Web.Clients.Pod.Models
{
    public class AddWithdrawRulePlanResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("creationDate")]
        [JsonConverter(typeof(MicrosecondEpochConverter))]
        public DateTime? CreationDate { get; set; }

        [JsonProperty("maxAmount")]
        public int MaxAmount { get; set; }

        [JsonProperty("maxCount")]
        public int MaxCount { get; set; }

        [JsonProperty("typeCode")]
        public string TypeCode { get; set; }

        [JsonProperty("subscriptionDaysAmount")]
        public int SubscriptionDaysAmount { get; set; }
    }
}