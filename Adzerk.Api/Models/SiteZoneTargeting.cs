using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adzerk.Api.Models
{
    public class SiteZoneTargeting
    {
        public long SiteId { get; set; }
        public long ZoneId { get; set; }
        public bool IsExclude { get; set; }
    }
}
