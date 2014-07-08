#Smarty Streets .Net Client

A .Net client library for the Smarty Streets LiveAddress Api.

[![Build status](https://ci.appveyor.com/api/projects/status/28verebh8s3rf22d)](https://ci.appveyor.com/project/rentlercorp/smarty-streets-net)

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
    <add key="SmaryStreetsAuthId" value="{auth id here}"/>
    <add key="SmaryStreetsAuthToken" value="{auth token here}"/>
  </appSettings>
</configuration>
```
