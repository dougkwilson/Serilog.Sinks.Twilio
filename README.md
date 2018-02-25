# Serilog.Sinks.Twilio - A Serilog sink that sends events over SMS via Twilio

[![Build status](https://ci.appveyor.com/api/projects/status/63p1e4llkl0jakli?svg=true)](https://ci.appveyor.com/project/dougkwilson/serilog-sinks-twilio)
[![NuGet Version](http://img.shields.io/nuget/v/Serilog.Sinks.Twilio.svg?style=flat)](https://www.nuget.org/packages/Serilog.Sinks.Twilio/) 
[![NuGet](https://img.shields.io/nuget/dt/Serilog.Sinks.Twilio.svg)](https://www.nuget.org/packages/Serilog.Sinks.Twilio/)
[![Documentation](https://img.shields.io/badge/docs-wiki-yellow.svg)](https://github.com/serilog/serilog/wiki)
[![Join the chat at https://gitter.im/serilog/serilog](https://img.shields.io/gitter/room/serilog/serilog.svg)](https://gitter.im/serilog/serilog)
[![Help](https://img.shields.io/badge/stackoverflow-serilog-orange.svg)](http://stackoverflow.com/questions/tagged/serilog)

__Package__ - [Serilog.Sinks.Twilio](https://www.nuget.org/packages/serilog.sinks.twilio)
| __Platforms__ - .NET 4.5, .NET Standard 1.4

## Table of contents

- [Install via NuGet](#install-via-nuget)
- [Super simple to use](#super-simple-to-use)
- [Super simple to configure](#super-simple-to-configure)

---

## Install via NuGet

If you want to include the Twilio sink in your project, you can [install it directly from NuGet](https://www.nuget.org/packages/Serilog.Sinks.Twilio/).

To install the sink, run the following command in the Package Manager Console:

```
PM> Install-Package Serilog.Sinks.Twilio
```

## Super simple to use

In the following example, the sink will transmit log events to `<toPhoneNumber>` over SMS.

```csharp
  var log = new LoggerConfiguration()
    .WriteTo.Twilio(
      accountSid: "<your_account_sid>", 
      authToken: "<your_auth_token>", 
      fromPhoneNumber: "<fromPhoneNumber>", 
      toPhoneNumber: "<toPhoneNumber>",
      restrictedToMinimumLevel: LogEventLevel.Information)
    .CreateLogger();

  log.Information("Hello World!");
```

## Super simple to configure

Used in conjunction with [Serilog.Settings.Configuration](https://github.com/serilog/serilog-settings-configuration) the same sink can be configured in the following way:

```json
{
  "Serilog": {
    "MinimumLevel": "Fatal",
    "WriteTo": [
      {
        "Name": "Twilio",
        "Args": {
          "accountSid": "your_account_sid",
          "authToken": "your_auth_token",
          "fromPhoneNumber": "from_phone_number",
          "toPhoneNumber": "to_phone_number"
        }
      }
    ]
  }
}
```

## Credit
