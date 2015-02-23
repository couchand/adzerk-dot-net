using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Adzerk.Api.Models;
using Jil;
using RestSharp;

namespace Adzerk.Api
{
    public interface IClient
    {
        string CreateReport(IReport report);
        ReportResult PollForResult(string id);
        Task<ReportResult> RunReport(IReport report);

        IEnumerable<AdType> ListAdTypes();
        IEnumerable<Advertiser> ListAdvertisers();
        IEnumerable<Campaign> ListCampaigns();
        IEnumerable<Channel> ListChannels();
        IEnumerable<Creative> ListAdvertiserCreatives(long advertiserId);
        IEnumerable<Flight> ListFlights();
        IEnumerable<Flight> ListCampaignFlights(long campaignId);
        IEnumerable<Login> ListLogins();
        IEnumerable<Priority> ListPriorities();
        IEnumerable<Publisher> ListPublishers();
        IEnumerable<Site> ListSites();
        IEnumerable<Zone> ListZones();
    }

    public class Client : IClient
    {
        public const int CURRENT_VERSION = 1;
        public const int POLL_DELAY = 1000;

        string apiKey;
        RestClient client;

        public Client(string apiKey)
        {
            this.apiKey = apiKey;

            var url = String.Format("http://api.adzerk.net/v{0}", CURRENT_VERSION);
            this.client = new RestClient(url);
        }

        private void addHeader(RestRequest request)
        {
            request.AddHeader("X-Adzerk-ApiKey", apiKey);
        }

        public string CreateReport(IReport report)
        {
            var request = new RestRequest("report/queue", Method.POST);

            addHeader(request);

            var serialized = ReportSerializer.SerializeReport(report);
            request.AddParameter("criteria", serialized);

            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var message = String.Format("Adzerk API error: {0}", response.StatusDescription);
                throw new AdzerkApiException(message, response);
            }

            try
            {
                var result = JSON.DeserializeDynamic(response.Content);
                return result.Id;
            }
            catch (Exception ex)
            {
                throw new AdzerkApiException("Report result does not contain report Id.", ex, report);
            }
        }

        public class ReportResultWrapper
        {
            public int Status;
            public string Message;
            public ReportResult Result;
        }

        private ReportResultWrapper pollForResult(string id)
        {
            var request = new RestRequest("report/queue/{id}", Method.GET);
            request.AddUrlSegment("id", id);

            addHeader(request);

            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var message = String.Format("Adzerk API error: {0}", response.StatusDescription);
                throw new AdzerkApiException(message, response);
            }

            return JSON.Deserialize<ReportResultWrapper>(response.Content);
        }

        public ReportResult PollForResult(string id)
        {
            var res = pollForResult(id);

            if (res.Status == 1)
            {
                return null;
            }

            if (res.Status == 2)
            {
                return res.Result;
            }

            var message = String.Format("Adzerk API error: {0}", res.Message);
            throw new AdzerkApiException(message, res);
        }

        public async Task<ReportResult> RunReport(IReport report)
        {
            var id = CreateReport(report);

            var res = pollForResult(id);

            while (res.Status == 1)
            {
                await Task.Delay(POLL_DELAY);

                res = pollForResult(id);
            }

            if (res.Status == 2)
            {
                return res.Result;
            }

            var message = String.Format("Adzerk API error: {0}", res.Message);
            throw new AdzerkApiException(message, res);
        }

        public IEnumerable<T> List<T>(string resource)
        {
            var request = new RestRequest(resource);
            addHeader(request);

            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var message = String.Format("Adzerk API error: {0}", response.StatusDescription);
                throw new AdzerkApiException(message, response);
            }

            try
            {
                var result = (ResultWrapper<T>)JSON.Deserialize<ResultWrapper<T>>(response.Content);
                return result.items;
            }
            catch (Exception ex)
            {
                var message = String.Format("Adzerk client error deserializing \"{0}\"", resource);
                throw new AdzerkApiException(message, ex, response);
            }
        }

        public IEnumerable<AdType> ListAdTypes()
        {
            return List<AdType>("adtypes");
        }

        public IEnumerable<Advertiser> ListAdvertisers()
        {
            return List<Advertiser>("advertiser");
        }

        public IEnumerable<Creative> ListAdvertiserCreatives(long advertiserId)
        {
            var resource = String.Format("advertiser/{0}/creatives", advertiserId);
            var creatives = List<Creative>(resource);
            return creatives;
        }

        public IEnumerable<Campaign> ListCampaigns()
        {
            var campaigns = List<CampaignDTO>("campaign");
            return campaigns.Select(c => c.ToCampaign());
        }

        public IEnumerable<Channel> ListChannels()
        {
            var channels = List<ChannelDTO>("channel");
            return channels.Select(c => c.ToChannel());
        }

        public IEnumerable<Flight> ListFlights()
        {
            var flights = List<FlightDTO>("flight");
            return flights.Select(f => f.ToFlight());
        }

        public IEnumerable<Flight> ListCampaignFlights(long campaignId)
        {
            var resource = String.Format("campaign/{0}/flight", campaignId);
            var flights = List<FlightDTO>(resource);
            return flights.Select(f => f.ToFlight());
        }

        public IEnumerable<Login> ListLogins()
        {
            return List<Login>("login");
        }

        public IEnumerable<Priority> ListPriorities()
        {
            var priorities = List<PriorityDTO>("priority");
            return priorities.Select(p => p.ToPriority());
        }

        public IEnumerable<Publisher> ListPublishers()
        {
            var publishers = List<PublisherDTO>("publisher");
            return publishers.Select(p => p.ToPublisher());
        }

        public IEnumerable<Site> ListSites()
        {
            return List<Site>("site");
        }

        public IEnumerable<Zone> ListZones()
        {
            return List<Zone>("zone");
        }
    }
}
