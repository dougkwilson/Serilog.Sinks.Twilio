using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog.Events;

namespace Serilog.Sinks.Twilio.Tests
{
	[TestClass]
	public class Tests
	{
		[TestMethod]
		public void CanBeConfigured() {
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

			Assert.AreEqual(String.Empty, sb.ToString());
		}
	}
}
