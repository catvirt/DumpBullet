using System;
using System.IO;
using System.IO.Compression;
using Leaf.xNet;

namespace RepoDumperionio
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" Enter REPO URL: ");

            var repo = Console.ReadLine();

            Console.WriteLine(" Enter API KEY: ");

            var apikey = Console.ReadLine();

            try
            {
                HttpRequest req = new HttpRequest
                {
                    AcceptEncoding = "gzip, deflate",
                    Authorization = apikey
                };

                var GetFilesZip = req.Get(repo, null).ToBytes();

                var zip = new ZipArchive(new MemoryStream(GetFilesZip), ZipArchiveMode.Read);

                foreach (var entry in zip.Entries)
                {
                    Directory.CreateDirectory(entry
                        .ToString()
                        .Substring(0, entry
                        .ToString()
                        .IndexOf("\\")));

                    File.AppendAllText($"{entry}", new StreamReader(entry
                        .Open())
                        .ReadToEnd());
                }
            }
            catch  { throw new Exception(" Error while dumping!");  }

            Console.WriteLine("Configs DUMPED! Press ANY key to close!");

            Console.Read();

        }
    }
}
