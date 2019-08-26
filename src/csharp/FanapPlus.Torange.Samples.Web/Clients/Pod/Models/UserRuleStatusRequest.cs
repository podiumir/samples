using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanapPlus.Torange.Samples.Web.Clients.Pod.Models
{
    public class GetUserRuleStatusRequest
    {
        public string AccessToken { get; set; }
        public int RuleId { get; set; }
    }
}
