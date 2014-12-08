using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adzerk.Api.Models
{
    class AdzerkApiException : ApplicationException
    {
        public dynamic Context;

        public AdzerkApiException(string message) : base(message)
        {
        }

        public AdzerkApiException(string message, dynamic context) : base(message)
        {
            this.Context = context;
        }
    }
}
