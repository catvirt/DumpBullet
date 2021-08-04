using Leaf.xNet;
using System;
using System.IO;
using System.IO.Compression;

namespace DumpBullet
{
    internal static class Program
    {
        /// <summary>
        /// Shit code but it works!
        /// </summary>
        private static void Main()
        {

            Console.WriteLine(" Input the repostory url: ");

            var readUrl = Console.ReadLine();

            Console.WriteLine(" Input the repostory API key: ");

            var apiKey = Console.ReadLine();

            try
            {

                var _client = new HttpRequest() { Authorization = apiKey };

                var getConfigs = _client.Get(readUrl).ToBytes();

                var zip = new ZipArchive(new MemoryStream(getConfigs), ZipArchiveMode.Read);

                Directory.CreateDirectory(zip.Entries.ToString().Substring(0, zip.Entries.ToString().IndexOf("\\")));

                foreach (var entry in zip.Entries)
                {
                    File.AppendAllText($"{entry}", new StreamReader( stream: entry.Open() ).ReadToEnd());
                }

            }
            catch
            {
                throw new Exception(" Error meanwhile I tried to dump the repo!");
            }

            Console.WriteLine(" Repo dump was a success! ");
        }
    }
}
