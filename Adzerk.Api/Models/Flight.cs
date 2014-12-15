using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adzerk.Api.Models
{
    public enum GoalType : int
    {
        Impressions = 1,
        Percentage = 2,
        Click = 3,
        AnyConversions = 4
    }

    public enum RateType : int
    {
        Flat = 1,
        CPM = 2,
        CPC = 3,
        CPAView = 4,
        CPAClick = 5,
        CPABoth = 6
    }

    public enum CapType : int
    {
        Impressions = 1,
        Clicks = 2,
        Conversions = 3
    }

    public enum FreqCapType : int
    {
        Hour = 1,
        Day = 2
    }

    public enum DeliveryStatus : int
    {
        Pending = 0,
        Healthy = 1,
        BorderLine = 2,
        InDanger = 3,
        Finished = 4,
        Underdelivered = 5
    }

    public class Flight
    {
        public long Id;
        public long CampaignId;
        public long PriorityId;
        public string Name;

        public DateTime StartDate;
        public DateTime? EndDate;
        public bool NoEndDate;

        public decimal Price;
        public long Impressions;

        public bool IsUnlimited;
        public bool IsDeleted;
        public bool IsActive;
        public bool IsCompanion;

        public IEnumerable<string> Keywords;
        public IEnumerable<string> UserAgentKeywords;

        public GoalType GoalType;
        public RateType? RateType;

        public CapType? CapType;
        public long? DailyCapAmount;
        public long? LifetimeCapAmount;

        public bool IsFreqCap;
        public long? FreqCap;
        public long? FreqCapDuration;
        public FreqCapType FreqCapType;

        public string DatePartingStartTime;
        public string DatePartingEndTime;

        public bool IsSunday;
        public bool IsMonday;
        public bool IsTuesday;
        public bool IsWednesday;
        public bool IsThursday;
        public bool IsFriday;
        public bool IsSaturday;

        public IEnumerable<GeoTargeting> GeoTargeting;
        public IEnumerable<SiteZoneTargeting> SiteZoneTargeting;
        public string CustomTargeting;

        public bool IsECPMOptimized;
        public int? ECPMOptimizePeriod;
        public decimal? ECPMMultiplier;
        public decimal? FloorECPM;
        public decimal? CeilingECPM;
        public decimal? DefaultECPM;
        public long? ECPMBurnInImpressions;

        public DeliveryStatus DeliveryStatus;

        public string CustomFieldsJson;
    }

    public class FlightDTO
    {
        public long Id;
        public long CampaignId;
        public long PriorityId;
        public string Name;

        public string StartDate;
        public string EndDate;
        public bool NoEndDate;

        public decimal Price;
        public long Impressions;

        public bool IsUnlimited;
        public bool IsDeleted;
        public bool IsActive;
        public bool IsCompanion;

        public string Keywords;
        public string UserAgentKeywords;

        public int GoalType;
        public int? RateType;

        public int? CapType;
        public long? DailyCapAmount;
        public long? LifetimeCapAmount;

        public bool IsFreqCap;
        public long? FreqCap;
        public long? FreqCapDuration;
        public int FreqCapType;

        public string DatePartingStartTime;
        public string DatePartingEndTime;

        public bool IsSunday;
        public bool IsMonday;
        public bool IsTuesday;
        public bool IsWednesday;
        public bool IsThursday;
        public bool IsFriday;
        public bool IsSaturday;

        public IEnumerable<GeoTargeting> GeoTargeting;
        public IEnumerable<SiteZoneTargeting> SiteZoneTargeting;
        public string CustomTargeting;

        public bool IsECPMOptimized;
        public int? ECPMOptimizePeriod;
        public decimal? ECPMMultiplier;
        public decimal? FloorECPM;
        public decimal? CeilingECPM;
        public decimal? DefaultECPM;
        public long? ECPMBurnInImpressions;

        public int DeliveryStatus;

        public string CustomFieldsJson;

        public Flight ToFlight()
        {
            var f = new Flight();

            f.Id = Id;
            f.CampaignId = CampaignId;
            f.PriorityId = PriorityId;
            f.Name = Name;

            f.StartDate = DateTime.Parse(StartDate);
            if (!NoEndDate)
            {
                f.EndDate = DateTime.Parse(EndDate);
            }
            f.NoEndDate = NoEndDate;

            f.Price = Price;
            f.Impressions = Impressions;

            f.IsUnlimited = IsUnlimited;
            f.IsDeleted = IsDeleted;
            f.IsActive = IsActive;
            f.IsCompanion = IsCompanion;

            f.Keywords = Keywords.Split(',').Select(k => k.Trim());
            if (UserAgentKeywords != null)
            {
                f.UserAgentKeywords = UserAgentKeywords.Split(',').Select(k => k.Trim());
            }
            else
            {
                f.UserAgentKeywords = new List<string>();
            }

            f.GoalType = (GoalType)GoalType;
            f.RateType = (RateType)RateType;

            f.CapType = (CapType)CapType;
            f.DailyCapAmount = DailyCapAmount;
            f.LifetimeCapAmount = LifetimeCapAmount;

            f.IsFreqCap = IsFreqCap;

            if (IsFreqCap)
            {
                f.FreqCap = FreqCap;
                f.FreqCapDuration = FreqCapDuration;
                f.FreqCapType = (FreqCapType)FreqCapType;
            }

            f.DatePartingStartTime = DatePartingStartTime;
            f.DatePartingEndTime = DatePartingEndTime;

            f.IsSunday = IsSunday;
            f.IsMonday = IsMonday;
            f.IsTuesday = IsTuesday;
            f.IsWednesday = IsWednesday;
            f.IsThursday = IsThursday;
            f.IsFriday = IsFriday;
            f.IsSaturday = IsSaturday;

            f.GeoTargeting = GeoTargeting;
            f.SiteZoneTargeting = SiteZoneTargeting;
            f.CustomTargeting = CustomTargeting;

            f.IsECPMOptimized = IsECPMOptimized;
            f.ECPMOptimizePeriod = ECPMOptimizePeriod;
            f.ECPMMultiplier = ECPMMultiplier;
            f.FloorECPM = FloorECPM;
            f.CeilingECPM = CeilingECPM;
            f.DefaultECPM = DefaultECPM;
            f.ECPMBurnInImpressions = ECPMBurnInImpressions;

            f.DeliveryStatus = (DeliveryStatus)DeliveryStatus;

            f.CustomFieldsJson = CustomFieldsJson;

            return f;
        }
    }
}
