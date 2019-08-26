using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanapPlus.Torange.Samples.Web.Utilities
{
    public interface ISessionUtilities
    {
        string CurrentUserAccessToken { get;}
        int CurrentUserId { get; }
    }
}
