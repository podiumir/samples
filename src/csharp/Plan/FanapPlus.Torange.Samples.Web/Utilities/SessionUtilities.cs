using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FanapPlus.Torange.Samples.Web.Options;
using Microsoft.Extensions.Options;

namespace FanapPlus.Torange.Samples.Web.Utilities
{
    public class SessionUtilities : ISessionUtilities
    {
        private readonly IOptions<MockConfig> _options;

        public SessionUtilities(IOptions<MockConfig> options)
        {
            _options = options;
        }

        public string CurrentUserAccessToken => _options.Value.AccessToken;
        public int CurrentUserId => _options.Value.UserId;
    }
}
