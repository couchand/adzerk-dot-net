using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adzerk.Api.Models
{
    public class ReportResult
    {
        public string StartDate;
        public string EndDate;

        public IEnumerable<IDictionary<string, dynamic>> Critieria;  // sic

        public IEnumerable<ReportRecord> Records;
        public IEnumerable<ReportOption> OptionRecords;

        public bool IsTotal;
        public IEnumerable<string> Grouping;
        public int TotalImpressions;
        public int TotalClicks;
        public double TotalCTR;
        public decimal TotalRevenue;
        public decimal TotalTrueRevenue;
        public decimal TotalECPM;
    }

    public class ReportRecord
    {
        public int Impressions;
        public int Clicks;
        public IDictionary<string, int> Events;
        public decimal Revenue;
        public decimal TrueRevenue;
        public string Date;
        public string FirstDate;
        public string LastDate;
        public double CTR;
        public IEnumerable<ReportDetail> Details;
    }

    public class ReportDetail
    {
        public string Title;
        public int Impressions;
        public int Clicks;
        public IDictionary<string, int> Events;
        public decimal Revenue;
        public decimal TrueRevenue;
        public string Date;
        public string FirstDate;
        public string LastDate;
        public double CTR;
        public IEnumerable<ReportDetail> Details;
        public ReportGrouping Grouping;
        public decimal eCPM;
    }

    public class ReportGrouping
    {
        public long OptionId;
        public long BrandId;
        public long CampaignId;
        public long SiteId;
        public long ZoneId;
        public long CreativeId;
        public long PublisherAccountId;
        public long AdTypeId;
        public long ChannelId;
        public long Date;
        public string DateType;
        public long MetroCode;
        public string Keyword;
        public long PriorityId;
        public long RateTypeId;
        public string Price;
    }

    public class ReportOption
    {
        public string Title;
        public int Impressions;
        public int Clicks;
        public decimal Revenue;
        public decimal TrueRevenue;
        public string Date;
        public string FirstDate;
        public string LastDate;
        public double CTR;
        public IEnumerable<ReportOptionDetail> Details;
        public decimal eCPM;
    }

    public class ReportOptionDetail
    {
        public int Impressions;
        public int Clicks;
        public decimal Revenue;
        public decimal TrueRevenue;
        public string Date;
        public string FirstDate;
        public string LastDate;
        public double CTR;
        public IEnumerable<ReportOptionDetail> Details;
        public decimal eCPM;
    }
}
