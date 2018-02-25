using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Serilog.Events;
using Xunit;

namespace Serilog.Sinks.Twilio.Tests
{
	public class TwilioSink_Can_Be
	{
		[Fact(Skip = "Requires valid Twilio account")]
		public void Configured_Programatically() {
			var sb = new StringBuilder();
			Debugging.SelfLog.Enable(msg => sb.AppendLine(msg));

			var log = new LoggerConfiguration()
				.WriteTo.Twilio(
					accountSid: "your_account_sid",
					authToken: "your_auth_token",
					fromPhoneNumber: "+14158141829",
					toPhoneNumber: "+16518675309",
					restrictedToMinimumLevel: LogEventLevel.Information)
				.CreateLogger();

			log.Information("Hello World!");

			Assert.Equal(String.Empty, sb.ToString());
		}

		[Fact(Skip = "Requires valid Twilio account")]
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
