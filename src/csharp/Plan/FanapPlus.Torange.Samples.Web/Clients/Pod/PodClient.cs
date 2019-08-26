using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FanapPlus.Torange.Samples.Web.Clients.Exceptions;
using FanapPlus.Torange.Samples.Web.Clients.Pod.Models;
using FanapPlus.Torange.Samples.Web.Options;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FanapPlus.Torange.Samples.Web.Clients.Pod
{
    public class PodClient : IPodClient
    {
        public const string PodHttpClientName = "PodHttpClient";
        private readonly IOptions<PodConfig> _options;
        private readonly IHttpClientFactory _clientFactory;

        public PodClient(IOptions<PodConfig> options, IHttpClientFactory clientFactory)
        {
            _options = options;
            _clientFactory = clientFactory;
        }

        private HttpClient CreateClient()
        {
            return _clientFactory.CreateClient(PodHttpClientName);
        }

        public async Task<AddWithdrawRulePlanResponse> AddWithdrawRulePlanAsync(AddWithdrawRulePlanRequest request)
        {
            HttpClient client = CreateClient();

            var url = $"{_options.Value.ApiBaseAddress}/nzh/biz/addWithdrawRulePlan";
            using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, url))
            {

                httpRequest.Headers.Add("_token_", new List<string>() { _options.Value.Token });
                httpRequest.Headers.Add("_token_issuer_", new List<string>() { "1" });

                var parameters = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("subscriptionDays", request.SubscriptionDays + ""),
                    new KeyValuePair<string, string>("maxAmount", request.MaxAmount + ""),
                    new KeyValuePair<string, string>("maxCount", request.MaxCount + ""),
                    new KeyValuePair<string, string>("typeCode", request.TypeCode + ""),
                    new KeyValuePair<string, string>("businessId", _options.Value.BusinessId + "")
                };
                httpRequest.Content = new FormUrlEncodedContent(parameters);

                var httpResponse = await client.SendAsync(httpRequest);

                var body = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    //Handle errors
                    throw new ApiException($"An error occurred: ${body}");
                }

                var result = JsonConvert.DeserializeObject<PodResult<AddWithdrawRulePlanResponse>>(body);
                if (result.HasError)
                {
                    throw new ApiException($"An error occurred: ${body}");
                }

                return result.Result;
            }
        }

        public async Task<bool> RevokeAsync(RevokeRuleRequest request)
        {
            HttpClient client = CreateClient();

            var url = $"{_options.Value.ApiBaseAddress}/nzh/biz/revokeWithdrawRule";
            using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, url))
            {
                httpRequest.Headers.Add("_token_", new List<string>() { _options.Value.Token });
                httpRequest.Headers.Add("_token_issuer_", new List<string>() { "1" });

                var parameters = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("ruleId", request.RuleId + ""),
                };
                httpRequest.Content = new FormUrlEncodedContent(parameters);

                var httpResponse = await client.SendAsync(httpRequest);

                var body = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    //Handle errors
                    throw new ApiException($"An error occurred: ${body}");
                }

                var result = JsonConvert.DeserializeObject<PodResult<bool>>(body);
                if (result.HasError)
                {
                    throw new ApiException($"An error occurred: ${body}");
                }

                return result.Result;
            }
        }

        public async Task<GetUserRuleStatusResponse> GetUserRuleStatusAsync(GetUserRuleStatusRequest request)
        {
            HttpClient client = CreateClient();

            var url = $"{_options.Value.ApiBaseAddress}/nzh/withdrawRuleUsageReport?ruleId={request.RuleId}";
            using (var httpRequest = new HttpRequestMessage(HttpMethod.Get, url))
            {
                httpRequest.Headers.Add("_token_", new List<string>() { request.AccessToken });
                httpRequest.Headers.Add("_token_issuer_", new List<string>() { "1" });
                    
                var httpResponse = await client.SendAsync(httpRequest);

                var body = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new ApiException($"An error occurred: ${body}");
                }

                var result = JsonConvert.DeserializeObject<PodResult<GetUserRuleStatusResponse>>(body);
                if (result.HasError)
                {
                    throw new ApiException($"An error occurred: ${body}");
                }

                return result.Result;
            }
        }

        public async Task<List<Rule>> GetRulesAsync(GetRulesRequest request)
        {
            HttpClient client = CreateClient();

            var url = $"{_options.Value.ApiBaseAddress}/nzh/biz/withdrawRuleList?userId={request.UserId}" +
                $"&offset=0&size=50";

            using (var httpRequest = new HttpRequestMessage(HttpMethod.Get, url))
            {
                httpRequest.Headers.Add("_token_", new List<string>() { _options.Value.Token });
                httpRequest.Headers.Add("_token_issuer_", new List<string>() { "1" });

                var httpResponse = await client.SendAsync(httpRequest);

                var body = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new ApiException($"An error occurred: ${body}");
                }

                var result = JsonConvert.DeserializeObject<PodResult<List<Rule>>>(body);
                if (result.HasError)
                {
                    return new List<Rule>();
                }

                return result.Result;
            }
        }
    }
}
