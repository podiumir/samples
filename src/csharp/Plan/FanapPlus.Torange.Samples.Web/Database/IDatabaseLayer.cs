using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanapPlus.Torange.Samples.Web.Database
{
    public interface IDatabaseLayer
    {
        Task<int> GetCurrentUserSubscriptionPlanIdAsync();
        Task<int> GetSubscriptionPlanIdAsync();
    }
}
