using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanapPlus.Torange.Samples.Web.Clients.Pod.Models
{
    public class AddWithdrawRulePlanRequest
    {
        public string TypeCode { get; set; }
        public int? SubscriptionDays { get; set; }
        public int? MaxAmount { get; set; }
        public int? MaxCount { get; set; }
    }
}
