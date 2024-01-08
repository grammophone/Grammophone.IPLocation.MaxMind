using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Grammophone.IPLocation.MaxMind
{
	internal class IPAddressJsonConverter : JsonConverter<IPAddress>
	{
		public override IPAddress Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (IPAddress.TryParse(reader.GetString(), out var ipAddress))
			{
				return ipAddress;
			}
			else
			{
				return null;
			}
		}

		public override void Write(Utf8JsonWriter writer, IPAddress value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToString());
		}
	}
}
