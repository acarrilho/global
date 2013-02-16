using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Global.Global;
using Global.Http;
using Global.Security;
using Global.Serialization;

namespace Global.Test.Console
{
    #region 
    [CollectionDataContract(Namespace = "", Name = "servers", ItemName = "server")]
    public class Servers : List<Server> { }

    public enum ServerStatus
    {
        IDLE = 1,
        OK = 2,
        ERROR = 3,
        WARNING = 4,
        PENDING = 5
    }

    [DataContract(Namespace = "", Name = "server", IsReference = true)]
    public class Server
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "displayname")]
        public string DisplayName { get; set; }

        [DataMember(Name = "domain")]
        public string Domain { get; set; }

        [DataMember(Name = "isencoder")]
        public bool IsEncoder { get; set; }

        [DataMember(Name = "isspare")]
        public bool IsSpare { get; set; }

        [DataMember(Name = "status")]
        public ServerStatus Status { get; set; }

        [DataMember(Name = "ip")]
        public string Ip { get; set; }

        [DataMember(Name = "originserverid")]
        public int? OriginServerId { get; set; }

        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "secretkey")]
        public string SecretKey { get; set; }

        [DataMember(Name = "lastupdatedon")]
        public DateTime? LastUpdatedOn { get; set; }

        [DataMember(Name = "lastupdatedby")]
        public string LastUpdatedBy { get; set; }

        [DataMember(Name = "lastmonitorresult")]
        public string LastMonitorResult { get; set; }

        [DataMember(Name = "applications")]
        public Applications Applications { get; set; }

        public bool HasUrlsRunning()
        {
            return Applications.Any(item => item.Urls.Any());
        }
    }
    [CollectionDataContract(Namespace = "", Name = "applications", ItemName = "application")]
    public class Applications : List<Application> { }

    [DataContract(Namespace = "", Name = "application")]
    public class Application
    {
        public Application()
        {
            Urls = new ServiceAssetUrls();
        }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "displayname")]
        public string DisplayName { get; set; }

        [DataMember(Name = "path")]
        public string Path { get; set; }

        [DataMember(Name = "serverid")]
        public int ServerId { get; set; }

        [DataMember(Name = "applicationtype")]
        public string ApplicationType { get; set; }

        [DataMember(Name = "server")]
        public Server Server { get; set; }

        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "secretkey")]
        public string SecretKey { get; set; }

        [DataMember(Name = "urls")]
        public ServiceAssetUrls Urls { get; set; }
    }
    /// <summary>
    /// Container Type to be used by DataContractSerializer, to build the Xml string accordingly to the specified contract with third Party applications
    /// </summary>
    [DataContract(Namespace = "", Name = "assetscontainer")]
    public class ThirdPartyServiceAssetsContainer
    {
        public ThirdPartyServiceAssetsContainer()
        {
            Assets = new List<ThirdPartyServiceAsset>();
        }

        [DataMember(Name = "assets")]
        public List<ThirdPartyServiceAsset> Assets { get; set; }
    }

    /// <summary>
    ///  Container Type to be used by DataContractSerializer, to build the Xml string accordingly to the specified contract with third Party applications
    /// </summary>
    [DataContract(Namespace = "", Name = "assetcontainer")]
    public class ThirdPartyServiceAssetContainer
    {
        [DataMember(Name = "asset")]
        public ThirdPartyServiceAsset Asset { get; set; }
    }
    /// <summary>
    /// Version of the Asset Entity with only Asset and Urls. This version does not has the Devices per Url.
    /// Used by the 3rd Party Applications
    /// </summary>
    [DataContract(Namespace = "", Name = "asset")]
    public class ThirdPartyServiceAsset : BaseServiceAsset
    {
        [DataMember(Name = "urls")]
        public BaseServiceAssetUrls Urls { get; set; }
    }
    [DataContract(Namespace = "", Name = "asset")]
    public class BaseServiceAsset
    {
        [DataMember(Name = "availableout")]
        public bool AvailableOut { get; set; }

        [DataMember(Name = "displayname")]
        public string DisplayName { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "imageurl")]
        public string ImageUrl { get; set; }

        [DataMember(Name = "isactive")]
        public bool IsActive { get; set; }

        [DataMember(Name = "isdefault")]
        public bool IsDefault { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "order")]
        public int Order { get; set; }
    }
    [CollectionDataContract(Namespace = "", Name = "assetsUrl", ItemName = "assetUrl")]
    public class BaseServiceAssetUrls : List<BaseServiceAssetUrl> { }

    [CollectionDataContract(Namespace = "", Name = "assetsUrl", ItemName = "assetUrl")]
    public class ServiceAssetUrls : List<ServiceAssetUrl> { }

    [DataContract(Namespace = "", Name = "assetUrl")]
    public class BaseServiceAssetUrl
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "isactive")]
        public bool IsActive { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }
    }

    [DataContract(Namespace = "", Name = "assetUrl")]
    public class ServiceAssetUrl : BaseServiceAssetUrl
    {
        [DataMember(Name = "assetid")]
        public int AssetId { get; set; }

        [DataMember(Name = "assetname")]
        public string AssetName { get; set; }

        [DataMember(Name = "deviceid")]
        public int DeviceId { get; set; }

        [DataMember(Name = "devices")]
        public ServiceDevices ServiceDevices { get; set; }

        [DataMember(Name = "publishingpointname")]
        public string PublishingPointName { get; set; }

        [DataMember(Name = "application")]
        public Application Application { get; set; }

        [DataMember(Name = "applicationid")]
        public int? ApplicationId { get; set; }

        [DataMember(Name = "encoder")]
        public Server Encoder { get; set; }

        [DataMember(Name = "encoderid")]
        public int? EncoderId { get; set; }

        [DataMember(Name = "status")]
        public AssetUrlStatus Status { get; set; }
    }

    public enum AssetUrlStatus
    {
        IDLE = 1,
        OK = 2,
        ERROR = 3
    }
    [CollectionDataContract(Namespace = "", Name = "devices", ItemName = "devices")]
    public class ServiceDevices : List<ServiceDevice> { }

    [DataContract(Namespace = "", Name = "device")]
    public class ServiceDevice
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "useragents")]
        public ServiceUserAgents UserAgents { get; set; }
        [DataMember(Name = "useragentid")]
        public int UserAgentId { get; set; }
    }
    [CollectionDataContract(Namespace = "", Name = "useragents", ItemName = "useragent")]
    public class ServiceUserAgents : List<ServiceUserAgent> { }

    [DataContract(Namespace = "", Name = "useragent")]
    public class ServiceUserAgent
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "deviceid")]
        public int DeviceId { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
    #endregion



    public class Program
    {
        static void Main(string[] args)
        {
            // To avoid exiting
            System.Console.ReadLine();
        }

        private static void Test()
        {
            var r = new Http.Http("https://web.vodott.vodafone.pt/NewWatchPageLiveWebService/Assets.svc/get/")
                .SetUserAgent("CompanionTV_Vodafone_Android_")
                .SetAccept("application/json")
                .SetResponseEncoding(Encoding.UTF8).DoRequest<Result<ThirdPartyServiceAssetsContainer>>(Format.Json);
            System.Console.WriteLine(r.Successful);

            var theContent = new Http.Http("http://192.168.1.20:80/media-vod-hls/green_test/Green_Lantern_Trailer-m3u8-aapl.ism/QualityLevels(637000)/Keys(Green_Lantern_Trailer-m3u8-aapl,format=m3u8-aapl)")
                .SetContentType("application/octet-binary-data")
                .SetMethod("GET").DoRequest();
            System.Console.WriteLine(theContent);
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
