using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Jil;

namespace Adzerk.Api.Models
{
    public interface IReport
    {
        DateTime GetStartDate();
        DateTime GetEndDate();
        bool IsTop30();
        bool Exclude3rdParty();
        bool IsTotal();
        IEnumerable<string> GetGroupBy();
        IEnumerable<IDictionary<string, string>> GetParameters();
    }

    public class Report : IReport
    {
        public static List<string> VALID_GROUPS = new List<string>
        {
            "day", "week", "month",
            "advertiserId", "brandId", "campaignId", "optionId",
            "flightId", "creativeId", "adTypeId", "siteId", "zoneId",
            "countryCode", "metroCode", "keyword"
        };

        public static List<string> VALID_PARAMETERS = new List<string>
        {
            "advertiserId", "brandId", "campaignId",
            "flightId", "creativeId", "channelId",
            "adTypeId", "siteId", "zoneId",
            "countryCode", "metroCode", "keyword"
        };

        DateTime startDate, endDate;
        bool isTop30, exclude3rdParty, isTotal;
        List<string> groupBy;
        Dictionary<string, string> parameters;

        public DateTime GetStartDate()
        {
            return startDate;
        }

        public DateTime GetEndDate()
        {
            return endDate;
        }

        public bool IsTop30()
        {
            return isTop30;
        }

        public bool Exclude3rdParty()
        {
            return exclude3rdParty;
        }

        public bool IsTotal()
        {
            return isTotal;
        }

        public IEnumerable<string> GetGroupBy()
        {
            return groupBy;
        }

        public IEnumerable<IDictionary<string, string>> GetParameters()
        {
            var ps = new List<IDictionary<string, string>>();

            foreach (var pair in parameters)
            {
                var crit = new Dictionary<string, string>();
                crit[pair.Key] = pair.Value;
                ps.Add(crit);
            }

            return ps;
        }

        public Report(DateTime startDate, DateTime endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;

            this.groupBy = new List<string>();
            this.parameters = new Dictionary<string, string>();
        }

        public void SetIsTop30()
        {
            isTop30 = true;
        }

        public void SetExclude3rdParty()
        {
            exclude3rdParty = true;
        }

        public void SetIsTotal()
        {
            isTotal = true;
        }

        public void AddGroupBy(string group)
        {
            if (!VALID_GROUPS.Contains(group))
            {
                throw new AdzerkApiException(String.Format("Invalid group: '{0}'.", group));
            }

            groupBy.Add(group);
        }

        public void AddParameter(string key, string value)
        {
            if (!VALID_PARAMETERS.Contains(key))
            {
                throw new AdzerkApiException(String.Format("Invalid parameter: '{0}'.", key));
            }

            parameters.Add(key, value);
        }
    }

    public class ReportSerializer
    {
        public static string SerializeReport(IReport report)
        {
            var r = new {
                StartDate = report.GetStartDate(),
                EndDate = report.GetEndDate(),
                IsTop30 = report.IsTop30(),
                Exclude3rdParty = report.Exclude3rdParty(),
                IsTotal = report.IsTotal(),
                GroupBy = report.GetGroupBy(),
                Parameters = report.GetParameters()
            };

            using (var output = new StringWriter())
            {
                JSON.Serialize(r, output);
                return output.ToString();
            }
        }
    }
}
