using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adzerk.Api.Models
{
    public class Campaign
    {
        public long Id;
        public long AdvertiserId;
        public long? SalespersonId;
        public string Name;
        public bool IsDeleted;
        public bool IsActive;
        public decimal Price;
        public DateTime StartDate;
        public DateTime EndDate;
        public IEnumerable<Flight> Flights;
    }

    public class CampaignDTO
    {
        public long Id;
        public long AdvertiserId;
        public long? SalespersonId;
        public string Name;
        public bool IsDeleted;
        public bool IsActive;
        public decimal Price;
        public string StartDate;
        public string EndDate;
        public IEnumerable<FlightDTO> Flights;

        public Campaign ToCampaign()
        {
            var c = new Campaign();

            c.Id = Id;
            c.AdvertiserId = AdvertiserId;
            c.SalespersonId = SalespersonId;
            c.Name = Name;
            c.IsDeleted = IsDeleted;
            c.IsActive = IsActive;
            c.Price = Price;
            c.StartDate = DateTime.Parse(StartDate);
            c.EndDate = DateTime.Parse(EndDate);
            c.Flights = Flights.Select(f => f.ToFlight());

            return c;
        }
    }
}
