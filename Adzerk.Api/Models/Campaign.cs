using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adzerk.Api.Models
{
    public class Campaign
    {
        public long Id { get; set; }
        public long AdvertiserId { get; set; }
        public long? SalespersonId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IEnumerable<Flight> Flights { get; set; }
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
            if (EndDate != null)
            {
                c.EndDate = DateTime.Parse(EndDate);
            }

            if (Flights == null)
            {
                c.Flights = new List<Flight>();
            }
            else
            {
                c.Flights = Flights.Select(f => f.ToFlight());
            }

            return c;
        }
    }
}
