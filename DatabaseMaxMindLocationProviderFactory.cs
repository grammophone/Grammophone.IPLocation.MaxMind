using System;
using System.Collections.Generic;
using System.Text;

namespace Grammophone.IPLocation.MaxMind
{
	/// <summary>
	/// Factory for creating <see cref="DatabaseMaxMindLocationProvider"/> instances.
	/// </summary>
	public class DatabaseMaxMindLocationProviderFactory : ILocationProviderFactory
	{
		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="databaseFilename">The filename of the database to open for querying locations.</param>
		public DatabaseMaxMindLocationProviderFactory(string databaseFilename)
		{
			if (databaseFilename == null) throw new ArgumentNullException(nameof(databaseFilename));

			this.DatabaseFilename = databaseFilename;
		}

		/// <summary>
		/// The filename of the database to open for querying locations.
		/// </summary>
		public string DatabaseFilename { get; }
		
		/// <summary>
		/// Create a <see cref="DatabaseMaxMindLocationProvider"/> based on the given parameters.
		/// </summary>
		public ILocationProvider CreateLocationProvider() => throw new NotImplementedException();
	}
}
