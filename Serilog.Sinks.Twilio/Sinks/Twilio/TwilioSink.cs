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

using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using System;
using System.IO;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Serilog.Sinks.Twilio
{
	class TwilioSink : ILogEventSink
	{
		readonly Object _syncRoot = new Object();
		readonly ITextFormatter _textFormatter;
		readonly String _username;
		readonly String _password;
		readonly String _accountSid;
		private readonly PhoneNumber _fromPhoneNumber;
		private readonly PhoneNumber _toPhoneNumber;

		public TwilioSink(String username, String password, String accountSid, String fromPhoneNumber, String toPhoneNumber, ITextFormatter textFormatter) {
			_username = username ?? throw new ArgumentNullException(nameof(username));
			_password = password ?? throw new ArgumentNullException(nameof(password));
			_accountSid = accountSid;  // If accountSid is null the TwilioRestClient will automatically substitute the username
			if (String.IsNullOrWhiteSpace(fromPhoneNumber))
				throw new ArgumentException("Invalid fromPhoneNumber", nameof(fromPhoneNumber));
			_fromPhoneNumber = new PhoneNumber(fromPhoneNumber);
			if (String.IsNullOrWhiteSpace(toPhoneNumber))
				throw new ArgumentException("Invalid toPhoneNumber", nameof(toPhoneNumber));
			_toPhoneNumber = new PhoneNumber(toPhoneNumber);
			_textFormatter = textFormatter ?? throw new ArgumentNullException(nameof(textFormatter));

			TwilioClient.Init(username, password, accountSid);
		}

		public void Emit(LogEvent logEvent) {
			if (logEvent == null)
				throw new ArgumentNullException(nameof(logEvent));
			lock (_syncRoot) {

				using (var textWriter = new StringWriter()) {
					_textFormatter.Format(logEvent, textWriter);	

					var message = MessageResource.Create(
							to: _toPhoneNumber,
							from: _fromPhoneNumber,
							body: textWriter.ToString());
				}
			}
		}
	}
}
