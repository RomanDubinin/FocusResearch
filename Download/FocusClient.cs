using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FocusResearch
{
    public class FocusClient
    {
        private string key { get; }
        private HttpClient httpClient { get; }

        public FocusClient(Uri baseUri, string key)
        {
            this.key = key;
            httpClient = new HttpClient
            {
                BaseAddress = baseUri,
                Timeout = TimeSpan.FromSeconds(5)
            };
        }

        public async Task<string> ReadBookkeepingReportAsync(string inn, string ogrn)
        {
            return await httpClient.GetStringAsync($"buh?inn={inn}&ogrn={ogrn}&key={key}").ConfigureAwait(false);
        }
    }
}