using System;
using System.IO;
using System.Threading.Tasks;
using FocusResearch;

namespace Download
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var file = @"Files\Фокус-Контрагенты-2020-11-18.xlsx";
            var directory = Path.GetDirectoryName(file);
            var extendedRawDataFileName = Path.Combine(directory, Path.GetFileNameWithoutExtension(file) + "_extended_raw.txt");
            if (File.Exists(Path.Combine(directory, extendedRawDataFileName)))
                File.Delete(Path.Combine(directory, extendedRawDataFileName));
            var exelReader = new ExelReader();
            var identities = exelReader.GetOrganizationIdentifiers(file);
            Console.WriteLine(identities.Count + " organizations to process.");

            var focusClient = new FocusClient(new Uri("https://focus-api.kontur.ru/api3/"), args[0]);

            var i = 0;
            foreach (var identity in identities)
            {
                var bo = await focusClient.ReadBookkeepingReportAsync(identity.inn, identity.ogrn).ConfigureAwait(false);
                await File.AppendAllTextAsync(extendedRawDataFileName, MakeRawRow(identity, bo)).ConfigureAwait(false);
                Console.WriteLine(bo.Length);
                Console.WriteLine(i++);
                Console.WriteLine("----");
            }
        }

        private static string MakeRawRow((string inn, string ogrn) identity, string content)
        {
            return $"{identity.inn}, {identity.ogrn}, {content}\n";
        }
    }
}