using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Global;
using Global.Global;
using Global.Http;
using Global.Security;
using Global.Xml;

namespace TestWebRequest
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome. Don't know how to use? Type gt -h to see how!");

            //var program = new Program();
            HashSomething();
            //SendHttpRequest();
            //BuildUrl();
            //SendMail();
            //DataContractJsonSerialization();

            Console.ReadLine();
        }

        private static void HashSomething()
        {
            var toHash = string.Format("p235:123456789:910000000:{0}:bla_bla_bla_bla", Guid.NewGuid());
            byte[] privateKey;
            var hashed = new CryptoHelper().Encrypt(toHash, out privateKey);
            Console.WriteLine(hashed);

            var notHashed = new CryptoHelper().Decrypt(hashed, privateKey);
            Console.WriteLine(notHashed);
        }

        private static void BuildUrl()
        {
            var u = new UrlBuilder();
            var url = u.BaseUrl("http://www.google.pt/")
                .Parameters(p => p
                    .Add("parameter_a", "parameter_a_value")
                    .Add("parameter_b", "parameter_b_value")
                    .Add("name", "Andre Carrilho"))
                .Build();

            Console.WriteLine(url);
        }

        private static void SendMail()
        {
            var mailsWithDisplayNames = new Dictionary<string, string>
                       {
                           {"Another Person", "another.person@anothermail.com"},
                           {"Yet Another Person","yet.another.person@anothermail.com"}
                       };

            var justMails = new List<string>
                {
                    "wow.another.person@anothermail.com",
                    "ok.another.person@anothermail.com"
                };

            var mailHelper = new MailHelper("smtp.gmail.com", 587);
            mailHelper
                .From("Andre Carrilho", "me@mymail.com")
                .To(to => to.Add("Andre Carrilho", "anotherme@mymail.com"))
                .Bcc(bcc => bcc.Add(mailsWithDisplayNames))
                .Cc(cc => cc.Add(justMails))
                .Body("Trying out the MailHelper class with some Html: <p style='font-weight:bold;color:blue;font-size:32px;'>html</p>")
                .Subject("Testing Fluent MailHelper")
                .IsBodyHtml(true)
                .Credentials("someUser", "somePass")
                .Port(1234)
                .Ssl(true)
                .Send();

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
                    Console.Write("Type the output file path (or type enter to display the result on the console): ");
                    logPath = Console.ReadLine();

                    var http = new HttpHelper();

                    var loop = false;
                    string postData = null;
                    while(loop)
                    {
                        Console.WriteLine("How do you want to send the request [ GET | POST ]: ");
                        var method = Console.ReadLine();
                        loop = string.IsNullOrEmpty(method);
                    }

                    var addMore = false;
                    Console.Write("Do you wish to add header values [ y | n ]: ");
                    var addHeaders = Console.ReadLine();
                    if(!string.IsNullOrEmpty(addHeaders) || addHeaders.ToLower().Equals("y")) addMore = true;
                    while (addMore)
                    {
                        Console.Write("Key name: ");
                        var key = Console.ReadLine();

                        Console.Write("Key value: ");
                        var value = Console.ReadLine();

                        if (http.WebHeaderCollection == null) http.WebHeaderCollection = new System.Net.WebHeaderCollection();
                        http.WebHeaderCollection.Add(key, value);

                        Console.WriteLine();
                        Console.Write("Do you wish to add more header values [true | false]: ");
                        addMore = bool.Parse(Console.ReadLine());
                    }

                    string content = http.SendRequest(site, postData, EnumHelper.StringToEnum<HttpRequestType>(""), null, null,
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

        private static void DataContractJsonSerialization()
        {
            var obj = new SampleObject
                          {
                              Id = Guid.NewGuid(),
                              Name = "André",
                              AnotherObject = new SampleObject.SubObject
                                                  {
                                                      Age = 24,
                                                      Birthdate = DateTime.Now
                                                  }
                          };

            var jsonSer = new DataContractSerializerHelper<SampleObject>();
            var stringObj = jsonSer.SerializeToJsonString(obj, Encoding.UTF8);
            
            Console.WriteLine(stringObj);
            Console.WriteLine("Done serializing to json");

            var deserializedObj = jsonSer.DeserializeFromJsonString(stringObj, Encoding.UTF8);

            Console.WriteLine(deserializedObj.Id);
            Console.WriteLine(deserializedObj.Name);
            Console.WriteLine(deserializedObj.AnotherObject.Age);
            Console.WriteLine(deserializedObj.AnotherObject.Birthdate);
            Console.WriteLine("Done deserializing to json");

            var path = Console.ReadLine();
            jsonSer.SerializeToJsonFile(path, obj, Encoding.UTF8);

            var deserializeFiledObj = jsonSer.DeserializeFromJsonFile(path, Encoding.UTF8);

            Console.WriteLine(deserializeFiledObj.Id);
            Console.WriteLine(deserializeFiledObj.Name);
            Console.WriteLine(deserializeFiledObj.AnotherObject.Age);
            Console.WriteLine(deserializeFiledObj.AnotherObject.Birthdate);
            Console.WriteLine("Done deserializing from json file");
        }
    }
}
