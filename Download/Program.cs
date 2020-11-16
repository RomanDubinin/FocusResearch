using System;
using System.Threading.Tasks;
using FocusResearch;

namespace Download
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var focusClient = new FocusClient(new Uri("https://focus-api.kontur.ru/api3/"), args[0]);
            var bo = await focusClient.ReadBookkeepingReportAsync("6164079184", "1026103281500").ConfigureAwait(false);
            Console.WriteLine(bo);
        }
    }
}