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

                HttpRequest httpRequest = new HttpRequest
                {
                    AcceptEncoding = "gzip, deflate",
                    Authorization = apiKey
                };
                byte[] buffer = httpRequest.Get(readUrl, null).ToBytes();
                ZipArchive zipArchive = new ZipArchive(new MemoryStream(buffer), ZipArchiveMode.Read);
                foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
                {
                    Directory.CreateDirectory(zipArchiveEntry.ToString().Substring(0, zipArchiveEntry.ToString().IndexOf("\\")));
                    File.AppendAllText(string.Format("{0}", zipArchiveEntry), new StreamReader(zipArchiveEntry.Open()).ReadToEnd());
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
