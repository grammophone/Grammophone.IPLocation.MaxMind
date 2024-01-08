using System;
using System.Collections.Generic;
using System.Text;
using MaxMind.GeoIP2;

namespace Grammophone.IPLocation.MaxMind
{
	/// <summary>
	/// Location provider that uses MaxMind web services.
	/// </summary>
	public class WebServiceMaxMindLocationProvider : MaxMindLocationProvider<WebServiceClient>
	{
		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="accountID">The MaxMind account ID.</param>
		/// <param name="licenseKey">The MaxMind license key.</param>
		public WebServiceMaxMindLocationProvider(int accountID, string licenseKey)
			: base(new WebServiceClient(accountID, licenseKey))
		{
		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="accountID">The MaxMind account ID.</param>
		/// <param name="licenseKey">The MaxMind license key.</param>
		/// <param name="host">The host of the web service.</param>
		public WebServiceMaxMindLocationProvider(int accountID, string licenseKey, string host)
			: base(new WebServiceClient(accountID, licenseKey, host: host))
		{
		}

		/// <inheritdoc/>
		public override string ProviderName => nameof(WebServiceMaxMindLocationProvider);
	}
}
