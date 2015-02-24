using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adzerk.Api.Models
{
    public class Creative
    {
        public long Id { get; set; }
        public long AdvertiserId { get; set; }
        public long AdTypeId { get; set; }

        public string Body { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Alt { get; set; }

        public bool IsSync { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

        public bool IsHTMLJS { get; set; }
        public string ScriptBody { get; set; }
    }
}
