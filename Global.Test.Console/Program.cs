using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Global.Global;
using Global.Http;
using Global.Security;
using Global.Xml;

namespace Global.Test.Console
{

    public class Program
    {
        static void Main(string[] args)
        {
            //ReadHashValues();
            //HashSomething();
            //SendHttpRequest();
            //BuildUrl();
            //SendMail();
            DataContractSerialization();
            //Reflection();
            //Aes128();

            //Test();

            // To avoid exiting
            System.Console.ReadLine();
        }

        private static void Test()
        {
            var h = new HttpHelper();
            var s = h.SendGet(
                "http://192.168.1.20:80/media-vod-hls/green_test/Green_Lantern_Trailer-m3u8-aapl.ism/QualityLevels(637000)/Keys(Green_Lantern_Trailer-m3u8-aapl,format=m3u8-aapl)",
                "application/octet-binary-data");
            System.Console.WriteLine(s);
        }

        private static void Aes128()
        {

            var aes = new System.Security.Cryptography.AesManaged {KeySize = 128, BlockSize = 128};
            aes.GenerateKey();

            // Save hex-version
            using (var t = new StreamWriter("c:\\temp\\original-aes.key"))
            {
                t.WriteLine(BitConverter.ToString(aes.Key));
                t.WriteLine(BitConverter.ToString(aes.Key).Replace("-", ""));
            }

            System.Console.WriteLine("original : {0}", BitConverter.ToString(aes.Key));
            System.Console.WriteLine("original parsed : {0}", BitConverter.ToString(aes.Key).Replace("-", ""));

            // write the full bytes to a files
            ByteArrayToFile("c:\\temp\\original-aesbyte.key", aes.Key);

            // extract the bytes from the hex string generated above
            String[] arr = BitConverter.ToString(aes.Key).Split('-');
            var array = new byte[arr.Length];
            for (var i = 0; i < arr.Length; i++) array[i] = Convert.ToByte(arr[i], 16);

            // Save regenerated hex-version
            using (var t = new StreamWriter("c:\\temp\\regenerated-aes.key"))
            {
                t.WriteLine(BitConverter.ToString(array));
                t.WriteLine(BitConverter.ToString(array).Replace("-", ""));
            }

            System.Console.WriteLine("regenerated : {0}", BitConverter.ToString(array));
            System.Console.WriteLine("regenerated parsed : {0}", BitConverter.ToString(array).Replace("-", ""));

            ByteArrayToFile("c:\\temp\\regenerated-aesbyte.key", array);
            System.Console.WriteLine("hexa without -: {0}", BitConverter.ToString(array).Replace("-", ""));
        }
        /// <summary>
        /// Function to save byte array to a file
        /// </summary>
        /// <param name="fileName">File name to save byte array</param>
        /// <param name="byteArray">Byte array to save to external file</param>
        /// <returns>Return true if byte array save successfully, if not return false</returns>
        public static bool ByteArrayToFile(string fileName, byte[] byteArray)
        {
            try
            {
                // Open file for reading
                using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    // Writes a block of bytes to this stream using data from a byte array.
                    fileStream.Write(byteArray, 0, byteArray.Length);
                    // close file stream
                    fileStream.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Error
                System.Console.WriteLine("Exception caught in process: {0}", ex.ToString());
            }

            // error occured, return false
            return false;
        }

        private static void ReadHashValues()
        {
            var str =
                @"+mQHiA/xd3Uk6EJ5+37KnkfkjhcZM8gRd/mg04LhN9zQwoqiWgEhBnlS3+oktWDN9fXuo8HDk4stsBKL3lT6qkyhqPyomrB/8Nw6jMV5a7QuHs0e1GcM/8ob7GRGSdP+/YwIhrp6BeEeDZ9J3D0q8ZnTOlDmJQ3xpzqoajDb3Aq3283oaCKM0IQCIWWSPIlB6kz/SMS91Al9VixdW4kJbJfIU9BNV4w4uFHd1UXJSyuSA6ty5em/1lsBqr7jI90v";
            var c = new CryptoHelper { SecretKeyString = "6ea6db577fb840f8b516a65406eb58a9" };
            var hashed = c.Decrypt(str, 128, 128);
            System.Console.WriteLine(hashed);
        }

        private static void HashSomething()
        {
            var toHash = string.Format("p235:123456789:910000000:{0}:bla_bla_bla_bla", Guid.NewGuid());
            byte[] privateKey;
            var hashed = new CryptoHelper().Encrypt(toHash, out privateKey);
            System.Console.WriteLine(hashed);

            var notHashed = new CryptoHelper().Decrypt(hashed, privateKey);
            System.Console.WriteLine(notHashed);
        }

        private static void SendHttpRequest()
        {
            string site;
            string logPath;
            string exit = String.Empty;
            var method = "";

            while (!String.Equals(exit, "exit"))
            {
                try
                {
                    System.Console.Write("Type the url: ");
                    site = System.Console.ReadLine();
                    System.Console.Write("Type the output file path (or type enter to display the result on the console): ");
                    logPath = System.Console.ReadLine();

                    var http = new HttpHelper();

                    string postData = null;
                    System.Console.WriteLine("How do you want to send the request [ GET | POST ]: ");
                    method = System.Console.ReadLine();

                    var addMore = false;
                    System.Console.Write("Do you wish to add header values [ y | n ]: ");
                    var addHeaders = System.Console.ReadLine();
                    addMore = !string.IsNullOrEmpty(addHeaders) && addHeaders.ToLower().Equals("y");
                    while (addMore)
                    {
                        System.Console.Write("Key name: ");
                        var key = System.Console.ReadLine();

                        System.Console.Write("Key value: ");
                        var value = System.Console.ReadLine();

                        if (http.WebHeaderCollection == null) http.WebHeaderCollection = new System.Net.WebHeaderCollection();
                        http.WebHeaderCollection.Add(key, value);

                        System.Console.WriteLine();
                        System.Console.Write("Do you wish to add more header values [true | false]: ");
                        addMore = bool.Parse(System.Console.ReadLine());
                    }

                    var content = http.SendRequest(site, postData, EnumHelper.StringToEnum<HttpRequestType>(method),
                        null, null, true, null, null, null, Encoding.UTF8);

                    using (TextWriter txt = new StreamWriter(logPath))
                        txt.WriteLine(content);

                    //http.DownloadFile(site, logPath);

                    System.Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine("Everything went well.");
                }
                catch (Exception ex)
                {
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("Error: {0}", ex.Message);
                }

                System.Console.WriteLine();
                System.Console.ForegroundColor = ConsoleColor.Gray;
                System.Console.Write("Type 'exit' to leave or press [Enter] to send another request: ");
                exit = System.Console.ReadLine();
            }
        }

        private static void BuildUrl()
        {
            var u = new UrlBuilder();
            var url = u.BaseUrl("http://www.google.pt/")
                .Parameters(p => p
                    .Add("parameter_a", "parameter_a_value")
                    .Add("parameter_b", "parameter_b_value")
                    .Add("name", "André Carrilho"))
                .Build();

            System.Console.WriteLine(url);
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

            System.Console.ReadLine();
        }

        private static void DataContractSerialization()
        {
            var result = new Result<SampleObject>
                             {
                                 Code = "1234",
                                 Message = "message",
                                 DetailedMessage = "detailed message",
                                 Successful = true,
                                 Parameters = new List<Parameter>
                                                  {
                                                      new Parameter {Key = "key 1", Value = "value 1"},
                                                      new Parameter {Key = "key 2", Value = "value 2"}
                                                  },
                                 Value = new SampleObject
                                             {
                                                 Id = Guid.NewGuid(),
                                                 Name = "name"
                                             }
                             };

            //var dcSer = new DataContractSerializerHelper<Result<bool>>();
            //var ser = new SerializerHelper<Result<bool>>();

            var dcjson = DataContractSerializerHelper.ToJsonString(result); // dcSer.SerializeToJsonString(result);
            System.Console.WriteLine("DataContractSerializer (json) : ");
            System.Console.WriteLine(dcjson);

            System.Console.WriteLine("");

            var dcXml = DataContractSerializerHelper.ToXmlString(result); // dcSer.SerializeToString(result);
            System.Console.WriteLine("DataContractSerializer (xml) : ");
            System.Console.WriteLine(dcXml);

            System.Console.WriteLine("");

            var xml = XmlSerializerHelper.ToXmlString(result); // ser.SerializeToString(result);
            System.Console.WriteLine("XmlSerializer : ");
            System.Console.WriteLine(xml);
        }

        private static void Reflection()
        {
            var stuff = Activator.CreateInstance("TestWebRequest", "");
        }
        public abstract class AbstractStuff { public abstract void Print(string hello); }
        public class One : AbstractStuff { public override void Print(string hello) { System.Console.WriteLine(hello); } }
        public class Two : AbstractStuff { public override void Print(string hello) { System.Console.WriteLine(hello); } }
    }
}
