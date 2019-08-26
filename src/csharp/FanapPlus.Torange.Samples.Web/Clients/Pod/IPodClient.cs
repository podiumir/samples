using System.Collections.Generic;
using System.Threading.Tasks;
using FanapPlus.Torange.Samples.Web.Clients.Pod.Models;

namespace FanapPlus.Torange.Samples.Web.Clients.Pod
{
    public interface IPodClient
    {
        Task<AddWithdrawRulePlanResponse> AddWithdrawRulePlanAsync(AddWithdrawRulePlanRequest request);
        Task<GetUserRuleStatusResponse> GetUserRuleStatusAsync(GetUserRuleStatusRequest request);
        Task<List<Rule>> GetRulesAsync(GetRulesRequest request);
        Task<bool> RevokeAsync(RevokeRuleRequest request);
    }
}
