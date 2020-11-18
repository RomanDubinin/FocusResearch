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
            for (int i = 1; i <= 10; i++)
            {
                try
                {
                    return await httpClient.GetStringAsync($"buh?inn={inn}&ogrn={ogrn}&key={key}").ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Console.WriteLine("-----------------ERROR-----------------");
                    Console.WriteLine(e);
                    Console.WriteLine($"inn {inn}, ogrn{ogrn}");
                    Console.WriteLine("-----------------error-----------------");
                }

                await Task.Delay(TimeSpan.FromSeconds(Math.Max(Math.Pow(i, 2), 15))).ConfigureAwait(false);
            }
            throw new Exception("Error happentd. See logs for more info.");
        }
    }
}