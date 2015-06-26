Adzerk .NET API Client
======================

An unofficial API client for the [Adzerk API][0] written in C# for use
in .NET projects.

  * a warning
  * introduction
  * getting started
  * more information

a warning
---------

***This project is currently under active development.  Even though I'm
using it in production, I don't really recommend that you do.***

introduction
-------------

Adzerk exposes almost all of your campaign and advertiser data through
the API, but getting it is somewhat stressful:  there are numerous
quirks and intricacies to worry about (cf. "Criteria").  This library
wraps all that up in a neat little package for your dot-netting.

getting started
---------------

Install the package on [NuGet][1].  It's marked as development at the
moment because it's under development.  Bug reports and feature requests
welcome, pull requests even more welcome.

You'll want to use the library.

```csharp
using Adzerk.Api;
```

And you will need your API key from Adzerk to create a client.

```csharp
var adzerk = new Client("MY_API_KEY");
```

All of the various list methods are available to retrieve records.

```csharp
var sites = adzerk.ListSites();
var zones = adzerk.ListZones();
```

The reporting API is also available, implement `IReport` or just use
the simple report builder `Report`.

```csharp
using Adzerk.Api.Models;


var report = new Report
(
    new DateTime(2015, 1, 1),
    new DateTime(2015, 12, 31)
);

report.AddParameter("advertiserId", 1234);
report.AddParameter("campaignId", 6789);

report.AddGroupBy("flightId");  // API uses optionId but report builder fixes it

var result = await adzerk.RunReport(report);
```

more information
----------------

For more information, see the Adzerk [API Documentation][0].

[0]: https://github.com/adzerk/adzerk-api/wiki/
[1]: https://www.nuget.org/packages/Adzerk.Api
