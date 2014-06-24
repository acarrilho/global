44KB of helpers: http/rest, serialization/deserialization, extensions, io, email, security and a light curl/wget like cli for windows users!

## installation

couldn't be simpler. just reference **Global.dll** into your project and you're set to go.

## usage

### http

nowdays apps are designed to be rest oriented. most of them expose http api's so consumers/users can take advange of their services and/or consume 3rd party http api's. global tries to tackle this problem by building a simple abstraction layer.

building the api url is one. normaly one has a base url like http://api.example.com/ to avoid repeating it on other urls. the relative url differs according to the tasks one wants to do, eg: **categories/** or **orders/1** or even **products/?filter.Name=Tea&filter.Price.lt=4.59**. instead of using string.Format or += use UrlBuilder fluent syntax:

```csharp
var u = new UrlBuilder();
var url = u.BaseUrl("http://api.example.com/products/")
    .Parameters(p => p
        .Add("filter.Name", "Tea")
        .Add("filter.Price.lt", "4.59"))
    .Build();
```

which would output: ```http://api.example.com/products/?filter.Name=Tea&filter.Price.lt=4.59```.

another feature when developing these kinds of apps is actually making the http request, serializing the payload and response into the appropriate format (json or xml). Here's an example of how easy it is to set up and configure the helper:

```csharp
var http = new Http(u => u
        .BaseUrl("http://www.google.com/")
        .Parameters(p => p
            .Add("q", "Cute+Pictures+Of+Birds")
            .Add("hl", "en")))
    .SetContentType("text/html")
    .SetMethod("GET")
    .SetHeaders(h => h
        .AddRequestHeader("CustomUserAgent", "Global User Agent")
        .AddRequestHeader("Accept", "application/xml"));
```

as you can see, in the constructor you can use the UrlBuilder shown above.
most of the http settings are available through this helper. aside from the fluent syntax you can also set/ovewrite values using properties:

```csharp
http.ResponseEncoding = Encoding.UTF8;
http.UserAgent = "custom_user_agent";
```

to actually make the request just do ```var response = http.DoRequest();``` to get the response in string format. but this is not the real scenario, normally we want to get the response already parsed into the desired object even if it comes in xml or json format. for example, if you have the following object:

```csharp
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
```

which is returned in xml format from a rest api. to get the response already serialized we can just call ```var sampleObject = http.DoRequest<SampleObject>(Format.Xml);```. and now we can access it as a normal SampleObject by calling it’s properties:

```csharp
Console.WriteLine("{0} is {1} years old.", 
    sampleObject.Name, 
    sampleObject.AnotherObject.Age);
```

### serialization

you already saw this in action in the previous section. but you can use it independently. consider the above SampleObject we've created. to convert that object into a json/xml file:

```csharp
// saved on disk as json
DataContractSerializerHelper
    .ToJsonFile(@"C:\serialized.json", obj, Encoding.UTF8);
 
// saved on disk as xml
DataContractSerializerHelper
    .ToXmlFile(@"C:\serialized.xml", obj, Encoding.UTF8);
```

or you can convert it into a string:

```csharp
// saved on disk as json
var jsonString = DataContractSerializerHelper.ToJsonString(obj, Encoding.UTF8);
 
// saved on disk as xml
var xmlString = DataContractSerializerHelper.ToXmlString(obj, Encoding.UTF8);
```

in other cases, it's necessary to read json/xml files stored on disk:

```csharp
// deserialize from a json file
var json = DataContractSerializerHelper
    .FromJsonFile<SampleObject>(@"C:\serialized.json", Encoding.UTF8);
 
// deserialize from a xml file
var xml = DataContractSerializerHelper
    .FromXmlFile<SampleObject>(@"C:\serialized.xml");
```

the same applies to strings:

```csharp
// deserialize from a json string
var json = DataContractSerializerHelper
    .FromJsonString<SampleObject>(jsonString);
 
// deserialize from a xml string
var xml = DataContractSerializerHelper
    .FromXmlString<SampleObject>(xmlString);
```

the above examples use the ```DataContractSerializer``` but there's also available the ```XmlSerializer```.

### mail

for monitoring or reporting, mail is a main function in any application, big or small. and when doing it over and over we should have a way to do it in a very simple way. first, configure to whom to send mails:

```csharp
// Configure mails with Name and Email
var bcc = new Dictionary<string, string>
    {
        {"Person", "person@mail.com"},
        {"Another Person","another.person@mail.com"}
    };
// Or just the Emails
var cc = new List<string>
    {
        "wow.another.person@anothermail.com",
        "ok.another.person@anothermail.com"
    };
```

and using the fluent syntax configure other properties like SSL, Subject, etc and just send the email. 

```csharp
var mail = new Mail("smtp.gmail.com", 587);
mail.From("Andre Carrilho", "me@mymail.com")
    .To(to => to.Add("Andre Carrilho", "anotherme@mymail.com"))
    .Bcc(bcc => bcc.Add(bcc))
    .Cc(cc => cc.Add(cc))
    .IsBodyHtml(true)
    .Body("Html <p style='color:blue;font-size:32px;'>content</p>.")
    .Subject("Testing Fluent MailHelper")
    .Credentials("someUser", "somePass")
    .Port(1234)
    .Ssl(true)
    .Send();
```

it is that simple!

## an extra tidbit

everyone loves wget/curl but unfortunately on windows, unless you're using cygwin or mysysgit, you're out of luck. that's why http.exe was born. to use got to the **requester** folder and issue the appropriate command, eg:

```http.exe -u "http://www.google.com/" -ua "My_User_Agent" -m "GET" -ct -oc```

type ```http``` to learn all available arguments.
