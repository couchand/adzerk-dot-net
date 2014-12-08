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
        public IDictionary<string, dynamic> Grouping;
        public decimal eCPM;
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
