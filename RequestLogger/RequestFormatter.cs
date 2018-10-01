using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.AspNetCore.Http;
using RequestLogger.Helpers;

namespace RequestLogger
{
	/// <summary>
	/// Helper, builds formatted representation of request headers and body
	/// </summary>
	public class RequestFormatters: IDisposable
	{
		private StringBuilder _sb;
		private HttpRequest _request;

		/// <summary>
		/// Create instanse of RequestFormatters
		/// </summary>
		/// <param name="request">Current request</param>
		public RequestFormatters(HttpRequest request)
		{
			if (request == null)
			{
				throw  new ArgumentNullException(nameof(request));
			}
			_sb = new StringBuilder();
		}

		/// <summary>
		/// Apply format rules to "headers" part of request, and append it to inner string
		/// </summary>
		public void FormatHeaders()
		{
			foreach (var header in _request.Headers)
			{
				_sb.AppendLine($"{header.Key}: {header.Value}");
			}
		}

		/// <summary>
		/// Apply format rules to "body" part of request, and append it to inner string
		/// </summary>
		public void FormatBody()
		{
			var body = RequestHelpers.GetBodyContentAsStringAsync(_request).Result;
			if (body == null)
			{
				return;
			}
			_sb.AppendLine("=== BODY ===");
			_sb.AppendLine(body);
		}

		/// <summary>
		/// GetFormatted string after format operations
		/// </summary>
		/// <returns></returns>
		public string GetFormattedString()
		{
			var result = _sb.ToString();
			Clean();
			return result;
		}

		/// <summary>
		/// Clears formatter
		/// </summary>
		public void Clean()
		{
			_sb.Clear();
		}

		private void ReleaseUnmanagedResources()
		{
			_sb.Clear();
			_sb = null;
		}

		public void Dispose()
		{
			ReleaseUnmanagedResources();
			GC.SuppressFinalize(this);
		}

		~RequestFormatters()
		{
			ReleaseUnmanagedResources();
		}
	}
}
