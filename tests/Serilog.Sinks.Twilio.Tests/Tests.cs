using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Serilog.Events;
using Xunit;

namespace Serilog.Sinks.Twilio.Tests
{
	public class TwilioSink_Can_Be
	{
		[Fact]
		public void Configured_Programatically() {
			var sb = new StringBuilder();
			Debugging.SelfLog.Enable(msg => sb.AppendLine(msg));

			var log = new LoggerConfiguration()
				.WriteTo.Twilio(
					accountSid: "AC2789548ef551af68eba59e42171ecd69",
					authToken: "5c1b870b5a352aaa2fe64b7ee9f4819d",
					fromPhoneNumber: "+15005550006",
					toPhoneNumber: "+15005550000",
					restrictedToMinimumLevel: LogEventLevel.Information)
				.CreateLogger();

			log.Information("Hello World!");

			Assert.Equal(String.Empty, sb.ToString());
		}

		[Fact]
		public void Configured_With_Settings_File() {
			var sb = new StringBuilder();
			Debugging.SelfLog.Enable(msg => sb.AppendLine(msg));

			var configuration = new ConfigurationBuilder()
					.AddJsonFile("appsettings.json")
					.Build();

			var log = new LoggerConfiguration()
					.ReadFrom.Configuration(configuration)
					.CreateLogger();

			log.Fatal("Hello World!");

			Assert.Equal(String.Empty, sb.ToString());
		}
	}
}
