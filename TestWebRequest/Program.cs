using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Global.Global;
using Global.Http;

namespace TestWebRequest
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Program program = new Program();
            //SendHttpRequest();
            //BuildUrl();
            SendMail();

            Console.ReadLine();
        }

        private static void BuildUrl()
        {
            var u = new UrlBuilder();
            var url = u.BaseUrl("http://www.google.pt/")
                .Parameters(p => p
                    .Add("aName", "aValue")
                    .Add("bName", "bValue")
                    .Add("1", "1")
                    .Add("2", "2"))
                .Build();

            Console.WriteLine(url);
        }

        private static void SendMail()
        {
            Dictionary<string, string> l = new Dictionary<string, string>();
            l.Add("A B C", "a@b.c");
            l.Add("D E F", "d@e.f");

            var mailHelper = new MailHelper("smtp.gmail.com", 587);
            mailHelper
                .From("Andre Carrilho", "andrecarrilho@gmail.com")
                .To(to => to
                              .Add(l))
                //.Add("Andre Carrilho", "andre.carrilho@hotmail.com"))
                .Cc(bcc => bcc
                               .Add("andre.carrilho@spreadder.com"))
                .Body(
                "The MailHelper class we are going to use for spreadder is also DONE! Now, hre is some HTML to see if this REALLY works as it should: <p style='font-weight:bold;color:blue;font-size:32px;'>html</p>")
                .Subject("test fluent MailHelper class")
                .IsBodyHtml(true)
                .Credentials("someUser", "somePass")
                .Ssl(true);
                //.Send();

            Console.ReadLine();
        }

        private static void SendHttpRequest()
        {
            string site;
            string logPath;
            string exit = String.Empty;

            while (!String.Equals(exit, "exit"))
            {
                try
                {
                    Console.Write("Type the url: ");
                    site = Console.ReadLine();
                    Console.Write("Type the output file path: ");
                    logPath = Console.ReadLine();

                    HttpHelper http = new HttpHelper();

                    Console.WriteLine();
                    Console.Write("Do you wish to add header values [true | false]: ");
                    bool addMore = bool.Parse(Console.ReadLine());
                    while (addMore)
                    {
                        Console.Write("Key name: ");
                        string key = Console.ReadLine();

                        Console.Write("Key value: ");
                        string value = Console.ReadLine();

                        if (http.WebHeaderCollection == null) http.WebHeaderCollection = new System.Net.WebHeaderCollection();
                        http.WebHeaderCollection.Add(key, value);

                        Console.WriteLine();
                        Console.Write("Do you wish to add more header values [true | false]: ");
                        addMore = bool.Parse(Console.ReadLine());
                    }

                    string content = http.SendRequest(site, null, HttpRequestType.GET, null, null,
                        true, null, null, null, Encoding.UTF8);

                    using (TextWriter txt = new StreamWriter(logPath))
                        txt.WriteLine(content);

                    //http.DownloadFile(site, logPath);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Everything went well.");
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: {0}", ex.Message);
                }

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Type 'exit' to leave or press [Enter] to send another request: ");
                exit = Console.ReadLine();
            }
        }
    }
}
