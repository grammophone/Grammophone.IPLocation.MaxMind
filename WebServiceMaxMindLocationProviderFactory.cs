using System;
using System.Collections.Generic;
using System.Text;

namespace Grammophone.IPLocation.MaxMind
{
	/// <summary>
	/// Factory for creating <see cref="WebServiceMaxMindLocationProvider"/> instances.
	/// </summary>
	public class WebServiceMaxMindLocationProviderFactory : ILocationProviderFactory
	{
		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="accountID">The MaxMind account ID.</param>
		/// <param name="licenseKey">The MaxMind license key.</param>
		public WebServiceMaxMindLocationProviderFactory(int accountID, string licenseKey)
		{
			if (licenseKey == null) throw new ArgumentNullException(nameof(licenseKey));

			this.AccountID = accountID;
			this.LicenseKey = licenseKey;
		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="accountID">The MaxMind account ID.</param>
		/// <param name="licenseKey">The MaxMind license key.</param>
		/// <param name="host">The host of the web service.</param>
		public WebServiceMaxMindLocationProviderFactory(int accountID, string licenseKey, string host)
		{
			if (licenseKey == null) throw new ArgumentNullException(nameof(licenseKey));
			if (host == null) throw new ArgumentNullException(nameof(host));

			this.AccountID = accountID;
			this.LicenseKey = licenseKey;
			this.Host = host;
		}

		/// <summary>
		/// The MaxMind account ID.
		/// </summary>
		public int AccountID { get; }

		/// <summary>
		/// The MaxMind license key.
		/// </summary>
		public string LicenseKey { get; }

		/// <summary>
		/// The host of the web service.
		/// </summary>
		public string Host { get; }

		/// <summary>
		/// Create a <see cref="WebServiceMaxMindLocationProvider"/> based on the given parameters.
		/// </summary>
		public ILocationProvider CreateLocationProvider()
			=> this.Host switch
			{
				null => new WebServiceMaxMindLocationProvider(this.AccountID, this.LicenseKey),
				_ => new WebServiceMaxMindLocationProvider(this.AccountID, this.LicenseKey, this.Host)
			};
	}
}
