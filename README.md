#Smarty Streets .Net Client

[![Build status](https://ci.appveyor.com/api/projects/status/28verebh8s3rf22d)](https://ci.appveyor.com/project/rentlercorp/smarty-streets-net)

A .Net client library for the Smarty Streets LiveAddress Api, a totally awesome address verification service.

##Installing

You can use NuGet to install the library and all of its dependancies:

```
Install-Package Rentler.SmartyStreets
```

Or you can fork the project if you are interested in the source.

##Getting Started
You'll need security keys in order to do anything. Go to http://smartystreets.com/ to create an account. You can find your keys at https://smartystreets.com/account/keys.

Once you have those, you can set them in the SmartyStreetsClient in code:

```
var client = new SmartyStreetsClient(
   authId: "{auth id here}",
   authToken: "{auth token here}");
```

Or an appropriate web.config or app.config file, under AppSettings:

```
<configuration>
  <appSettings>
    <add key="SmartyStreetsAuthId" value="{auth id here}"/>
    <add key="SmartyStreetsAuthToken" value="{auth token here}"/>
  </appSettings>
</configuration>
```

##Usage

There are two endpoints to consider. The first is the LiveAddress API, which takes a street address and verifies it.

```
var client = new SmartyStreetsClient();
var results = new List<SmartyStreetsAddress>();

results.AddRange(
  (await client.GetStreetAddress(
  	street: "1600 pennsylvania",
  	city: "washington",
  	state: "dc",
  	zipcode: "")));

foreach (var item in results)
{
  Console.WriteLine(string.Format("{0} {1} {2} {3} {4}",
    item.components.primary_number,
    item.components.street_predirection,
    item.components.street_name,
    item.components.street_suffix,
    item.components.street_postdirection)
      .Replace("  ", " ").Trim());
}
```

That'll print:

> 1600 Pennsylvania Ave NW

The other is the ZIP Code Api, which allows you to identify cities using zip codes and vice-versa.

```
var client = new SmartyStreetsClient();
var results = new List<SmartyStreetsCityStateZipLookup>();

results.AddRange((await client.GetLookup(
  city: "portland",
  state: "or")));

foreach (var i in results)
{
	string cities = "";
	i.city_states.ForEach(c => cities += c.city + ", ");
	cities = cities.Trim().TrimEnd(',');
	Console.WriteLine(cities);

	string zipcodes = "";
	i.zipcodes.ForEach(z => zipcodes += z.zipcode + ", ");
	zipcodes = zipcodes.Trim().TrimEnd(',');
	Console.WriteLine(zipcodes);
}
```

Which will make this:

> Portland

> 97086, 97201, 97202, 97203, 97204, 97205, 97206, 97207, 97208, 97209, 97210, 97211, 97212, 97213, 97214, 97215, 97216, 97217, 97218, 97219, 97220, 97221, 97222,97223, 97224, 97225, 97227, 97228, 97229, 97230, 97231, 97232, 97233, 97236, 97238, 97239, 97240, 97242, 97256, 97258, 97266, 97267, 97268, 97269, 97280, 97281, 97282, 97283, 97286, 97290, 97291, 97292, 97293, 97294, 97296, 97298

A few more bits of usage are included in the Rentler.SmartyStreets.Example project.
