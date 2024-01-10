# Grammophone.IPLocation.MaxMind

This library provides implementations of `ILocationProvider` and `ILocationProviderFactory` interfaces
found in [Grammophone.IPLocation](https://github.com/grammophone/Grammophone.IPLocation) that use the MaxMind API's.

## Local databse querying
The `DatabaseMaxMindLocationProvider` and corresponding `DatabaseMaxMindLocationProviderFactory` classes enable querying via database files
of MaxMind's 'mmdb' format.

Example usage:
```CS
string databaseFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GeoLite2-City.mmdb");

using (var locationProvider = new DatabaseMaxMindLocationProvider(databaseFilename))
{
  var location = await locationProvider.GetLocationAsync(IPAddress.Parse("[some IP address]"));

  // Process the estimated location...
}
```

## Web services querying
The `WebServiceMaxMindLocationProvider` and corresponding `WebServiceMaxMindLocationProviderFactory` classes enable querying via MaxMind's web services.

Example usage:
```CS
using (var locationProvider = new WebServiceMaxMindLocationProvider(123456, "[your account key]"))
{
  var location = await locationProvider.GetLocationAsync(IPAddress.Parse("[some IP address]));

  // Process the estimated location...
}
```

## Caching and aggregating
Here is an example that uses caching and aggregating location providers to first try web API querying then falling back to database query if the former fails.
The caching capability is provided by the [Grammophone.IPLocation.Caching](https://github.com/grammophone/Grammophone.IPLocation.Caching) library
which should also be present in a sibling directory along the [Grammophone.IPLocation](https://github.com/grammophone/Grammophone.IPLocation) library.
```CS
string databaseFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GeoLite2-City.mmdb");

var locationProviderFactories = new ILocationProviderFactory[]
{
  new WebServiceMaxMindLocationProviderFactory(123456, "[your account key]"),
  new DatabaseMaxMindLocationProviderFactory(databaseFilename)
};

var aggregateLocationProviderFactory =
  new AggregateLocationProviderFactory(locationProviderFactories);

var memoryCacheOptions = new MemoryCacheOptions
{
  SizeLimit = 1024
};

using (var locationCache = new LocationCache(memoryCacheOptions, aggregateLocationProviderFactory))
{
  var firstLocation = await locationCache.GetLocationAsync(IPAddress.Parse("[some IP address]"));

  var secondLocation = await locationCache.GetLocationAsync(IPAddress.Parse("[the same IP address]"));

  Assert.AreEqual(firstLocation, secondLocation);
}
```

This library depends on [Grammophone.IPLocation](https://github.com/grammophone/Grammophone.IPLocation) being in a sibling directory.
