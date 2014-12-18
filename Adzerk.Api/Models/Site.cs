using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adzerk.Api.Models
{
    public class Site
    {
        public long Id { get; set; }
        public long? PublisherAccountId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
