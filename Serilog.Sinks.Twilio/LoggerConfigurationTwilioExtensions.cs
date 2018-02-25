// Copyright 2018 Serilog Contributors
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.Sinks.Twilio;
using Serilog.Core;

namespace Serilog
{
	/// <summary>
	/// Adds the WriteTo.Twilio() extension method to <see cref="LoggerConfiguration"/>.
	/// </summary>
	public static class LoggerConfigurationTwilioExtensions
	{
		const String _defaultOutputTemplate = "[{Level}] {Message}";

		/// <summary>
		/// Adds a sink that sends log events via Twilio.
		/// </summary>
		/// <param name="sinkConfiguration">Logger sink configuration.</param>
		/// <param name="accountSid">Twilio account SID (username)</param>
		/// <param name="authToken">Twilio auth token (password)</param>
		/// <param name="fromPhoneNumber">Twilio PhoneNumber sending the SMS message</param>
		/// <param name="toPhoneNumber">Phone Number that will receive the SMS message</param>
		/// <param name="restrictedToMinimumLevel">The minimum level for
		/// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.
		/// The default is <code>LogEventLevel.Error</code>.</param>
		/// <param name="levelSwitch">A switch allowing the pass-through minimum level
		/// to be changed at runtime.</param>
		/// <param name="outputTemplate">A message template describing the format used to write to the sink.
		/// the default is <code>"[{Level}] {Message}"</code>.</param>
		/// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
		/// <returns>Logger configuration, allowing configuration to continue.</returns>
		/// <exception cref="ArgumentNullException">A required parameter is null.</exception>
		public static LoggerConfiguration Twilio(
				this LoggerSinkConfiguration sinkConfiguration,
				String accountSid,
				String authToken,
				String fromPhoneNumber,
				String toPhoneNumber,
				LogEventLevel restrictedToMinimumLevel = LogEventLevel.Error,
				String outputTemplate = _defaultOutputTemplate,
				IFormatProvider formatProvider = null,
				LoggingLevelSwitch levelSwitch = null) {

			if (sinkConfiguration == null)
				throw new ArgumentNullException(nameof(sinkConfiguration));
			if (outputTemplate == null)
				throw new ArgumentNullException(nameof(outputTemplate));

			var textFormatter = new MessageTemplateTextFormatter(outputTemplate, formatProvider);

			return sinkConfiguration.Sink(new TwilioSink(accountSid, authToken, accountSid, fromPhoneNumber, toPhoneNumber, textFormatter), restrictedToMinimumLevel, levelSwitch);
		}
	}
}
