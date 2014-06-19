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

### an extra tidbit

everyone loves wget/curl but unfortunately on windows unless you're using cygwin or mysysgit you're out of luck. that's why http.exe was born. to use got to the **requester** folder and issue the appropriate command, eg:

```http.exe -u "http://www.google.com/" -ua "Sample_User_Agent" -m "GET" -ct "text/html" -oc```

type ```http``` to learn all available arguments.