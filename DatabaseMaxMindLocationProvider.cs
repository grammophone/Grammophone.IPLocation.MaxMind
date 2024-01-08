using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MaxMind.GeoIP2;

namespace Grammophone.IPLocation.MaxMind
{
	/// <summary>
	/// Location provider that uses MaxMind a database file.
	/// </summary>
	public class DatabaseMaxMindLocationProvider : MaxMindLocationProvider<DatabaseReader>
	{
		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="databaseFilename">The filename of the database to open for querying locations.</param>
		public DatabaseMaxMindLocationProvider(string databaseFilename)
			: base(new DatabaseReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, databaseFilename)))
		{
		}

		/// <inheritdoc/>
		public override string ProviderName => nameof(DatabaseMaxMindLocationProvider);
	}
}
