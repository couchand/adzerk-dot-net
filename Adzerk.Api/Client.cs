using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using Adzerk.Api.Models;
using Jil;
using RestSharp;

namespace Adzerk.Api
{
    public interface IClient
    {
        string CreateReport(IReport report);
        dynamic PollForResult(string id);
        void RunReport(IReport report, Action<ReportResult> callback);

        IEnumerable<AdType> ListAdTypes();
        IEnumerable<Advertiser> ListAdvertisers();
        IEnumerable<Campaign> ListCampaigns();
        IEnumerable<Channel> ListChannels();
        IEnumerable<Flight> ListFlights();
        IEnumerable<Login> ListLogins();
        IEnumerable<Priority> ListPriorities();
        IEnumerable<Publisher> ListPublishers();
        IEnumerable<Site> ListSites();
        IEnumerable<Zone> ListZones();
    }

    public class Client
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

            var result = JSON.DeserializeDynamic(response.Content);
            return result.Id;
        }

        public class ReportResultWrapper
        {
            public int Status;
            public ReportResult Result;
        }

        public ReportResultWrapper PollForResult(string id)
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

        public void RunReport(IReport report, Action<ReportResult> callback)
        {
            var id = CreateReport(report);

            var res = PollForResult(id);

            while (res.Status == 1)
            {
                Thread.Sleep(POLL_DELAY);

                res = PollForResult(id);
            }

            if (res.Status == 2)
            {
                callback(res.Result);
                return;
            }

            var message = String.Format("Adzerk API error: {0}", res);
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
                // TODO: restore typed deserialization when issue is resolved
                //var result = (ResultWrapper<T>)JSON.Deserialize<ResultWrapper<T>>(response.Content);
                var result = JSON.DeserializeDynamic(response.Content);
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
            return List<Publisher>("publisher");
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
