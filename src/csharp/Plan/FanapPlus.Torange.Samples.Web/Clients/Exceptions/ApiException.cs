﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanapPlus.Torange.Samples.Web.Clients.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string message): base(message)
        {
            
        }
    }
}
