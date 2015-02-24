using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adzerk.Api.Models
{
    public class Creative
    {
        long Id { get; set; }
        long AdvertiserId { get; set; }
        long AdTypeId { get; set; }

        string Body { get; set; }
        string Url { get; set; }
        string Title { get; set; }
        string Alt { get; set; }

        bool IsSync { get; set; }
        bool IsDeleted { get; set; }
        bool IsActive { get; set; }

        bool IsHTMLJS { get; set; }
        string ScriptBody { get; set; }
    }
}
