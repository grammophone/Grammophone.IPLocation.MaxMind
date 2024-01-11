using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Grammophone.IPLocation.Models;
using MaxMind.GeoIP2;

namespace Grammophone.IPLocation.MaxMind
{
	/// <summary>
	/// Abstract location provider that uses MaxMind API's.
	/// </summary>
	/// <typeparam name="P">The type of MaxMind location provider.</typeparam>
	public abstract class MaxMindLocationProvider<P> : ILocationProvider
		where P : IGeoIP2Provider, IDisposable
	{
		#region Private fields

		private static readonly JsonSerializerOptions jsonSerializationOptions;

		#endregion

		#region Construction

		static MaxMindLocationProvider()
		{
			jsonSerializationOptions = new JsonSerializerOptions();

			jsonSerializationOptions.Converters.Add(new IPAddressJsonConverter());
		}

		/// <summary>
		/// CReate.
		/// </summary>
		/// <param name="maxMindProvider">The MaxMind location provider.</param>
		public MaxMindLocationProvider(P maxMindProvider)
		{
			if (maxMindProvider == null) throw new ArgumentNullException(nameof(maxMindProvider));

			this.MaxMindProvider = maxMindProvider;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The MaxMind provider.
		/// </summary>
		public P MaxMindProvider { get; }

		/// <inheritdoc/>
		public abstract string ProviderName { get; }

		#endregion

		#region Public methods

		/// <inheritdoc/>
		public void Dispose() => this.MaxMindProvider.Dispose();

		/// <inheritdoc/>
		public Task<Location> GetLocationAsync(IPAddress ipAddress)
		{
			if (ipAddress == null) throw new ArgumentNullException(nameof(ipAddress));

			var cityResponse = this.MaxMindProvider.City(ipAddress);

			City city = null;

			if (cityResponse.City != null)
			{
				city = new City
				{
					Name = cityResponse.City.Name
				};
			}

			Country country = null;

			if (cityResponse.Country != null)
			{
				country = new Country
				{
					Name = cityResponse.Country.Name,
					IsoCode = cityResponse.Country.IsoCode
				};
			}

			Continent continent = null;

			if (cityResponse.Continent != null)
			{
				continent = new Continent
				{
					Name = cityResponse.Continent.Name,
					Code = cityResponse.Continent.Code
				};
			}

			Coordinates coordinates = null;

			if (cityResponse.Location != null)
			{
				if (cityResponse.Location.HasCoordinates && cityResponse.Location.Longitude.HasValue && cityResponse.Location.Latitude.HasValue)
				{
					coordinates = new Coordinates
					{
						Longitude = cityResponse.Location.Longitude.Value,
						Latitude = cityResponse.Location.Latitude.Value
					};
				}
			}

			var subdivisions = from s in cityResponse.Subdivisions
												 select new Subdivision
												 {
													 Name = s.Name,
													 IsoCode = s.IsoCode
												 };

			var location = new Location
			{
				City = city,
				Country = country,
				Continent = continent,
				Coordinates = coordinates,
				TimeZone = cityResponse.Location?.TimeZone,
				ProviderName = this.ProviderName,
				Timestamp = DateTime.UtcNow,
				Subdivisions = subdivisions.ToList(),
				Response = JsonSerializer.Serialize(cityResponse, jsonSerializationOptions)
			};

			return Task.FromResult(location);
		}

		#endregion
	}
}
