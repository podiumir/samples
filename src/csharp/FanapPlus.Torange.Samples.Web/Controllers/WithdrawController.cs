using System;
using System.Net;
using System.Threading.Tasks;
using FanapPlus.Torange.Samples.Web.Clients.Pod;
using FanapPlus.Torange.Samples.Web.Clients.Pod.Models;
using FanapPlus.Torange.Samples.Web.Database;
using FanapPlus.Torange.Samples.Web.Options;
using FanapPlus.Torange.Samples.Web.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using System.Linq;

namespace FanapPlus.Torange.Samples.Web.Controllers
{
    [Route("api/withdraw")]
    public class WithdrawController : Controller
    {
        private readonly IPodClient _podClient;
        private readonly IOptions<PodConfig> _podOptions;
        private readonly IDatabaseLayer _databaseLayer;
        private readonly ISessionUtilities _session;

        public WithdrawController(IPodClient podClient, 
            IOptions<PodConfig> podOptions,
            IDatabaseLayer databaseLayer,
            ISessionUtilities session)
        {
            _podClient = podClient;
            _podOptions = podOptions;
            _databaseLayer = databaseLayer;
            _session = session;
        }

        [HttpPost("plan")]
        public async Task<AddWithdrawRulePlanResponse> CreatePlanAsync()
        {
            try
            {
                var result = await _podClient.AddWithdrawRulePlanAsync(new AddWithdrawRulePlanRequest
                {
                    MaxAmount = 100,
                    MaxCount = 0,
                    SubscriptionDays = 30,
                    TypeCode = WithdrawRuleTypeCodes.SubscriptionAmount
                });

                return result;
            }
            catch (Exception e)
            {
                //Handle error
                throw;
            }
            
        }

        [HttpGet("issue")]
        public async Task<IActionResult> GoToIssuePageAsync()
        {
            //Get plan id from your configuration or database.
            var planId = await _databaseLayer.GetSubscriptionPlanIdAsync();

            var podUrl = $"{_podOptions.Value.WebBaseAddress}/v1/pbc/issueWithdrawRuleByPlan/" +
                         $"?businessId={_podOptions.Value.BusinessId}" +
                         $"&planId={planId}" +
                         "&redirectUri=" + 
                         //Store some information in url so we can restore required information to store issue result for user.
                         WebUtility.UrlEncode($"{Request.Scheme}://{Request.Host}/api/withdraw/issue/redirect"
                         );
            return Redirect(podUrl);
        }


        [HttpGet("issue/redirect")]
        public async Task<IActionResult> IssueRedirectAsync()
        {
            var rules = await _podClient.GetRulesAsync(new GetRulesRequest { UserId = _session.CurrentUserId });

            if (rules.Any())
            {
                //Store state of user in your database.
            }

            return Ok();
        }

        [HttpGet("issue/redirect")]
        public async Task<IActionResult> ResubscribeAsync()
        {
            var rules = await _podClient.GetRulesAsync(new GetRulesRequest { UserId = _session.CurrentUserId });
            var rule = rules.FirstOrDefault();

            if (rule.ExpireDate < DateTime.UtcNow)
            {
                //Redirect user to Pod confirmation page.
            }
            else
            {

            }

            return Ok();
        }

        [HttpPost("issue/revoke")]
        public async Task<IActionResult> RevokeAsync()
        {
            var rules = await _podClient.GetRulesAsync(new GetRulesRequest { UserId = _session.CurrentUserId });
            var rule = rules.FirstOrDefault();

            if (rule.TypeCode == WithdrawRuleTypeCodes.SubscriptionAmount)
            {
                if (rule.ExpireDate >= DateTime.UtcNow)
                {
                    var result = await _podClient.RevokeAsync(new RevokeRuleRequest {RuleId = rule.Id});
                }
            }
            else
            {
                var result = await _podClient.RevokeAsync(new RevokeRuleRequest { RuleId = rule.Id });
            }
            
            return Ok();
        }

    }
}
