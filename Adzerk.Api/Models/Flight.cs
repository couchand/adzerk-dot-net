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
        public long Id { get; set; }
        public long CampaignId { get; set; }
        public long PriorityId { get; set; }
        public string Name { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool NoEndDate { get; set; }

        public decimal Price { get; set; }
        public long Impressions { get; set; }

        public bool IsUnlimited { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsCompanion { get; set; }

        public IEnumerable<string> Keywords { get; set; }
        public IEnumerable<string> UserAgentKeywords { get; set; }

        public GoalType GoalType { get; set; }
        public RateType? RateType { get; set; }

        public CapType? CapType { get; set; }
        public long? DailyCapAmount { get; set; }
        public long? LifetimeCapAmount { get; set; }

        public bool IsFreqCap { get; set; }
        public long? FreqCap { get; set; }
        public long? FreqCapDuration { get; set; }
        public FreqCapType FreqCapType { get; set; }

        public string DatePartingStartTime { get; set; }
        public string DatePartingEndTime { get; set; }

        public bool IsSunday { get; set; }
        public bool IsMonday { get; set; }
        public bool IsTuesday { get; set; }
        public bool IsWednesday { get; set; }
        public bool IsThursday { get; set; }
        public bool IsFriday { get; set; }
        public bool IsSaturday { get; set; }

        public IEnumerable<GeoTargeting> GeoTargeting { get; set; }
        public IEnumerable<SiteZoneTargeting> SiteZoneTargeting { get; set; }
        public string CustomTargeting { get; set; }

        public bool IsECPMOptimized { get; set; }
        public int? ECPMOptimizePeriod { get; set; }
        public decimal? ECPMMultiplier { get; set; }
        public decimal? FloorECPM { get; set; }
        public decimal? CeilingECPM { get; set; }
        public decimal? DefaultECPM { get; set; }
        public long? ECPMBurnInImpressions { get; set; }

        public DeliveryStatus DeliveryStatus { get; set; }

        public string CustomFieldsJson { get; set; }
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
            if (!NoEndDate && EndDate != null)
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

            f.Keywords = (Keywords ?? "").Split(',').Select(k => k.Trim());
            f.UserAgentKeywords = (UserAgentKeywords ?? "").Split(',').Select(k => k.Trim());

            f.GoalType = (GoalType)GoalType;

            if (RateType.HasValue)
            {
                f.RateType = (RateType)RateType;
            }

            if (CapType.HasValue)
            {
                f.CapType = (CapType)CapType;
            }
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
