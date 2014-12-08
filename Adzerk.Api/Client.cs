using System;
using System.Net;
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
        IEnumerable<Login> GetLogins();

        string CreateReport(IReport report);
        dynamic PollForResult(string id);
        void RunReport(IReport report, Action<ReportResult> callback);
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

        private class LoginResultWrapper
        {
            public IEnumerable<Login> items;
        }

        public IEnumerable<Login> GetLogins()
        {
            var request = new RestRequest("login", Method.GET);
            addHeader(request);

            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var message = String.Format("Adzerk API error: {0}", response.StatusDescription);
                throw new AdzerkApiException(message, response);
            }

            try
            {
                var result = (LoginResultWrapper)JSON.Deserialize<LoginResultWrapper>(response.Content);
                return result.items;
            }
            catch (Exception ex)
            {
                var message = String.Format("Adzerk client error deserializing \"{0}\"", response.Content);
                throw new AdzerkApiException(message, response);
            }
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
    }
}
