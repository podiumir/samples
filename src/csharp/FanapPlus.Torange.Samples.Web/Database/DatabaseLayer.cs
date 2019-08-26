using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FanapPlus.Torange.Samples.Web.Options;
using Microsoft.Extensions.Options;

namespace FanapPlus.Torange.Samples.Web.Database
{
    public class DatabaseLayer : IDatabaseLayer
    {
        private readonly IOptions<MockConfig> _options;

        public DatabaseLayer(IOptions<MockConfig> options)
        {
            _options = options;
        }

        public async Task<int> GetCurrentUserSubscriptionPlanIdAsync()
        {
            return _options.Value.SubscriptionRuleId;
        }

        public async Task<int> GetSubscriptionPlanIdAsync()
        {
            return _options.Value.SubscriptionRuleId;
        }
    }
}
