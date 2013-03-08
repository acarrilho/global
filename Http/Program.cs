using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using Global;
using Global.IO;

namespace Http
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            try
            {

                DoRequest(ParseCommand(args));
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("");
                Console.WriteLine("An error as occured.");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private static void Header()
        {
            Console.WriteLine();
            Console.WriteLine("***************************************************************");
            Console.WriteLine("*             Name : http requester tool                      *");
            Console.WriteLine("*        Developer : Andre da Silva Carrilho                  *");
            Console.WriteLine("*             Date : 2013-02-17                               *");
            Console.WriteLine("*             Blog : http://www.acarrilho.com                 *");
            Console.WriteLine("*           Hosted : https://bitbucket.org/acarrilho/global   *");
            Console.WriteLine("***************************************************************");
            Console.WriteLine();
            Console.WriteLine("Type 'http -h' to display the available commands.");
            Console.WriteLine();
        }
        private static void HttpHelp()
        {
            Header();
            Console.WriteLine("How to configre the request: ");
            Console.WriteLine("  -h       (Help)");
            Console.WriteLine("           Display the list of available commands.");
            Console.WriteLine("           If this flag is present other flags will be ignored.");
            Console.WriteLine("  -u       (Url)");
            Console.WriteLine("           The request url.");
            Console.WriteLine("  -op      (OutputPath)");
            Console.WriteLine("           Outputs the response to a file.");
            Console.WriteLine("           By default the response is outputed to the console.");
            Console.WriteLine("  -oc      (OutputToScreen)");
            Console.WriteLine("           Defines if the response should be uotput to the console");
            Console.WriteLine("  -m       (Method)");
            Console.WriteLine("           Http verb. Accepted verbs are GET, POSt, PUT and DELETE.");
            Console.WriteLine("           Default is GET..");
            Console.WriteLine("  -ct      (ContentType)");
            Console.WriteLine("           The content type.");
            Console.WriteLine("  -t       (Timeout)");
            Console.WriteLine("           The request timeout.");
            Console.WriteLine("  -ua      (UserAgent)");
            Console.WriteLine("           The request user agent.");
            Console.WriteLine("  -ka      (KeepAlive)");
            Console.WriteLine("           Sets the keep alive flag.");
            Console.WriteLine("  -re      (ResponseEncoding)");
            Console.WriteLine("           Encoding to be used when decoding the response. Accepted values are:");
            Console.WriteLine("              -> ASCII");
            Console.WriteLine("              -> BigEndianUnicode");
            Console.WriteLine("              -> Default");
            Console.WriteLine("              -> Unicode");
            Console.WriteLine("              -> UTF32");
            Console.WriteLine("              -> UTF7");
            Console.WriteLine("              -> UTF8");
            Console.WriteLine("  -p       (Payload)");
            Console.WriteLine("           The actual payload.");
            Console.WriteLine("  -pe      (PayloadEncoding)");
            Console.WriteLine("           The encoding to be used when encoding the post data.");
            Console.WriteLine("              -> ASCII");
            Console.WriteLine("              -> BigEndianUnicode");
            Console.WriteLine("              -> Default");
            Console.WriteLine("              -> Unicode");
            Console.WriteLine("              -> UTF32");
            Console.WriteLine("              -> UTF7");
            Console.WriteLine("              -> UTF8");
            Console.WriteLine("  -pp      (PayloadPath)");
            Console.WriteLine("           The path of the file that contains the postData.");
            Console.WriteLine("           This is used for POST and PUT requests.");
            Console.WriteLine("           Typically you'd want to use xl or json.");
            Console.WriteLine("");
            Console.WriteLine("  -c:u     (Credentials:Username)");
            Console.WriteLine("           Sets the request credential username.");
            Console.WriteLine("  -c:o     (Credentials:Password)");
            Console.WriteLine("           Sets the request credential password.");
            Console.WriteLine("  -c:d     (Credentials:Domain)");
            Console.WriteLine("           Sets the request credential domain.");
            Console.WriteLine("");
            Console.WriteLine("  -ps:bl   (ProxySettings:BypssLocally)");
            Console.WriteLine("           Bypasses proxy settings for local requests.");
            Console.WriteLine("  -ps:a    (ProxySettings:Address)");
            Console.WriteLine("           Sets the proxy url address..");
            Console.WriteLine("  -ps:c:u  (Credentials:Username)");
            Console.WriteLine("           Sets the request credential username.");
            Console.WriteLine("  -ps:c:p  (Credentials:Password)");
            Console.WriteLine("           Sets the request credential password.");
            Console.WriteLine("  -ps:c:d  (Credentials:Domain)");
            Console.WriteLine("           Sets the request credential domain.");
            Console.WriteLine("  -ps:ab   (Credentials:AddBypass)");
            Console.WriteLine("           Adds bypass rules to the proxy settings.");
            Console.WriteLine("");
            Console.WriteLine("  -hd      (Headers)");
            Console.WriteLine("           Adds headers to the request.");
            Console.WriteLine("           Something similar to 'Accept-Language:da'.");
        }
        private static HttpArgs ParseCommand(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                var argsObj = new HttpArgs();

                for (var i = 0; i < args.Length; i++)
                {
                    switch (args[i])
                    {
                        case "-h":
                            argsObj.Help = true;
                            break;
                        case "-u":
                            if (string.IsNullOrEmpty(args[i + 1]))
                            {
                                Console.WriteLine("Must supply value for -u.");
                                return null;
                            }
                            argsObj.Url = args[i + 1];
                            break;
                        case "-oc":
                            argsObj.OutputToConsole = true;
                            break;
                        case "-op":
                            if (string.IsNullOrEmpty(args[i + 1]))
                            {
                                Console.WriteLine("Must supply value for -op.");
                                return null;
                            }
                            argsObj.OutputPath = args[i + 1];
                            break;
                        case "-m":
                            if (string.IsNullOrEmpty(args[i + 1]))
                            {
                                Console.WriteLine("Must supply value for -m.");
                                return null;
                            }
                            argsObj.Method = args[i + 1].ToUpper();
                            break;
                        case "-ct":
                            if (string.IsNullOrEmpty(args[i + 1]))
                            {
                                Console.WriteLine("Must supply value for -ct.");
                                return null;
                            }
                            argsObj.ContentType = args[i + 1];
                            break;
                        case "-t":
                            if (string.IsNullOrEmpty(args[i + 1]))
                            {
                                Console.WriteLine("Must supply value for -t.");
                                return null;
                            }
                            argsObj.Timeout = int.Parse(args[i + 1]);
                            break;
                        case "-ua":
                            if (string.IsNullOrEmpty(args[i + 1]))
                            {
                                Console.WriteLine("Must supply value for -ua.");
                                return null;
                            }
                            argsObj.UserAgent = args[i + 1];
                            break;
                        case "-ka":
                            if (string.IsNullOrEmpty(args[i + 1]))
                            {
                                Console.WriteLine("Must supply value for -ka.");
                                return null;
                            }
                            argsObj.KeepAlive = bool.Parse(args[i + 1]);
                            break;
                        case "-re":
                            if (string.IsNullOrEmpty(args[i + 1]))
                            {
                                Console.WriteLine("Must supply value for -re.");
                                return null;
                            }
                            argsObj.ResponseEncoding = Common.StringToEnum<Encoding>(args[i + 1]);
                            break;
                        case "-pe":
                            if (string.IsNullOrEmpty(args[i + 1]))
                            {
                                Console.WriteLine("Must supply value for -pde.");
                                return null;
                            }
                            argsObj.PayloadEncoding = Common.StringToEnum<Encoding>(args[i + 1]);
                            break;
                        case "-p":
                            if (string.IsNullOrEmpty(args[i + 1]))
                            {
                                Console.WriteLine("Must supply value for -p.");
                                return null;
                            }
                            argsObj.Payload = args[i + 1];
                            break;
                        case "-pp":
                            if (string.IsNullOrEmpty(args[i + 1]))
                            {
                                Console.WriteLine("Must supply value for -pp.");
                                return null;
                            }
                            argsObj.Payload = IOHelper.GetFileContent(args[i + 1]);
                            break;
                        case "-c:u":
                            if (string.IsNullOrEmpty(args[i + 1]))
                            {
                                Console.WriteLine("Must supply value for -c:u.");
                                return null;
                            }
                            argsObj.CredentialUsername = args[i + 1];
                            break;
                        case "-c:p":
                            if (string.IsNullOrEmpty(args[i + 1]))
                            {
                                Console.WriteLine("Must supply value for -c:p.");
                                return null;
                            }
                            argsObj.CredentialPassword = args[i + 1];
                            break;
                        case "-o:d":
                            if (string.IsNullOrEmpty(args[i + 1]))
                            {
                                Console.WriteLine("Must supply value for -o:d.");
                                return null;
                            }
                            argsObj.CredentialDomain = args[i + 1];
                            break;
                        case "-ps:bl":
                            if (string.IsNullOrEmpty(args[i + 1]))
                            {
                                Console.WriteLine("Must supply value for -ps:bl.");
                                return null;
                            }
                            argsObj.ProxyBypassLocally = bool.Parse(args[i + 1]);
                            break;
                        case "-ps:a":
                            if (string.IsNullOrEmpty(args[i + 1]))
                            {
                                Console.WriteLine("Must supply value for -ps:a.");
                                return null;
                            }
                            argsObj.ProxyAddress = args[i + 1];
                            break;
                        case "-ps:c:u":
                            if (string.IsNullOrEmpty(args[i + 1]))
                            {
                                Console.WriteLine("Must supply value for -ps:c:u.");
                                return null;
                            }
                            argsObj.ProxyCredentialUsername = args[i + 1];
                            break;
                        case "-ps:c:p":
                            if (string.IsNullOrEmpty(args[i + 1]))
                            {
                                Console.WriteLine("Must supply value for -ps.c.p.");
                                return null;
                            }
                            argsObj.ProxyCredentialPassword = args[i + 1];
                            break;
                        case "-ps:c:d":
                            if (string.IsNullOrEmpty(args[i + 1]))
                            {
                                Console.WriteLine("Must supply value for -ps:c:d.");
                                return null;
                            }
                            argsObj.ProxyCredentialDomain = args[i + 1];
                            break;
                        case "-ps:ab":
                            if (string.IsNullOrEmpty(args[i + 1]))
                            {
                                Console.WriteLine("Must supply value for -ps:ab.");
                                return null;
                            }
                            if (argsObj.ProxyAddBypassRule == null) argsObj.ProxyAddBypassRule = new ArrayList();
                            argsObj.ProxyAddBypassRule.Add(args[i + 1]);
                            break;
                        case "-hd":
                            if (string.IsNullOrEmpty(args[i + 1]))
                            {
                                Console.WriteLine("Must supply value for -ha.");
                                return null;
                            }
                            if (argsObj.Header == null) argsObj.Header = new WebHeaderCollection();
                            argsObj.Header.Add(args[i + 1]);
                            break;
                    }
                }
                return argsObj;
            }

            return null;

            //Console.WriteLine("Must call http command with valid arguments.");
            //HttpHelp();
        }
        private static bool ValidateArgs(HttpArgs args)
        {
            if (args == null)
            {
                Console.WriteLine("No values have been supplied");
                return false;
            }
            if (args.Help)
            {
                HttpHelp();
                return false;
            }
            if (string.IsNullOrEmpty(args.Url))
            {
                Console.WriteLine("Url must be supplied.");
                return false;
            }
            if (!string.IsNullOrEmpty(args.ProxyCredentialUsername) && !string.IsNullOrEmpty(args.ProxyCredentialPassword))
            {
                if (!string.IsNullOrEmpty(args.ProxyCredentialDomain))
                {
                    args.Proxy.Credentials = new NetworkCredential(args.ProxyCredentialUsername, args.ProxyCredentialPassword, args.ProxyCredentialDomain);
                }
                else
                {
                    args.Proxy.Credentials = new NetworkCredential(args.ProxyCredentialUsername, args.ProxyCredentialPassword);
                }
            }
            if (args.ProxyAddBypassRule != null && args.ProxyAddBypassRule.Count > 0)
            {
                foreach (var bypass in args.ProxyAddBypassRule) args.Proxy.BypassArrayList.Add(bypass);
            }

            return true;
        }
        private static void DoRequest(HttpArgs args)
        {
            if (ValidateArgs(args))
            {
                var http = new Global.Http.Http(args.Url)
                    .SetMethod(args.Method)
                    .SetContentType(args.ContentType)
                    //.SetPayload(args.Payload, args.PayloadEncoding)
                    .SetUserAgent(args.UserAgent);
                    //.SetCredentials(args.Credential)
                    //.SetProxy(args.Proxy)
                    //.SetResponseEncoding(args.ResponseEncoding);

                http.Payload = args.Payload;
                if (args.KeepAlive != null) http.KeepAlive = (bool)args.KeepAlive;
                if (args.Timeout != null) http.Timeout = (int)args.Timeout;
                if (args.Header != null && args.Header.Count > 0)
                    http.Headers = args.Header;

                var response = http.DoRequest();

                if (!string.IsNullOrEmpty(args.OutputPath))
                {
                    using (var writer = new StreamWriter(args.OutputPath)) writer.WriteLine(response);
                }
                else if (args.OutputToConsole)
                {
                    Console.WriteLine(response);
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("");
                Console.WriteLine("Request was successfully made.");
            }
        }
        private class HttpArgs
        {
            public bool Help { get; set; }
            public string Url { get; set; }
            public bool OutputToConsole { get; set; }
            public string OutputPath { get; set; }
            private string _method;
            public string Method
            {
                get { return _method ?? "GET"; }
                set { _method = value; }
            }
            public string ContentType { get; set; }
            public int? Timeout { get; set; }
            public string UserAgent { get; set; }
            public bool? KeepAlive { get; set; }
            private Encoding _responseEncoding;
            public Encoding ResponseEncoding { get { return _responseEncoding ?? Encoding.UTF8; } set { _responseEncoding = value; } }
            private Encoding _payloadEncoding;
            public Encoding PayloadEncoding { get { return _payloadEncoding ?? Encoding.UTF8; } set { _payloadEncoding = value; } }
            public string Payload { get; set; }
            public NetworkCredential Credential { get; private set; }

            public string CredentialUsername
            {
                set
                {
                    if (Credential == null) Credential = new NetworkCredential();
                    Credential.UserName = value;
                }
            }

            public string CredentialPassword
            {
                set
                {
                    if (Credential == null) Credential = new NetworkCredential();
                    Credential.Password = value;
                }
            }

            public string CredentialDomain
            {
                set
                {
                    if (Credential == null) Credential = new NetworkCredential();
                    Credential.Domain = value;
                }
            }

            public WebProxy Proxy { get; private set; }

            public bool ProxyBypassLocally
            {
                set
                {
                    if (Proxy == null) Proxy = new WebProxy();
                    Proxy.BypassProxyOnLocal = value;
                }
            }

            public string ProxyAddress
            {
                set
                {
                    if (Proxy == null) Proxy = new WebProxy();
                    Proxy.Address = new Uri(value);
                }
            }
            private string _proxyCredentialUsername;
            public string ProxyCredentialUsername
            {
                get
                {
                    return _proxyCredentialUsername;
                }
                set
                {
                    if (Proxy == null) Proxy = new WebProxy();
                    _proxyCredentialUsername = value;
                }
            }
            private string _proxyCredentialPassword;
            public string ProxyCredentialPassword
            {
                get
                {
                    return _proxyCredentialPassword;
                }
                set
                {
                    if (Proxy == null) Proxy = new WebProxy();
                    _proxyCredentialPassword = value;
                }
            }
            private string _proxyCredentialDomain;
            public string ProxyCredentialDomain
            {
                get
                {
                    return _proxyCredentialDomain;
                }
                set
                {
                    if (Proxy == null) Proxy = new WebProxy();
                    _proxyCredentialDomain = value;
                }
            }
            private ArrayList _proxyAddBypassRule;

            public HttpArgs()
            {
                KeepAlive = null;
                Timeout = null;
            }

            public ArrayList ProxyAddBypassRule
            {
                get
                {
                    return _proxyAddBypassRule;
                }
                set
                {
                    if (Proxy == null) Proxy = new WebProxy();
                    if (_proxyAddBypassRule == null) _proxyAddBypassRule = new ArrayList();
                    _proxyAddBypassRule.Add(value);
                }
            }

            public WebHeaderCollection Header { get; set; }
        }
    }
}
