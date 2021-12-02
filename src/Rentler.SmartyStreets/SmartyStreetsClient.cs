using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Rentler.SmartyStreets
{
    /// <summary>
    /// Handles requests to SmartyStreets for street address
    /// and city/state/zip lookups.
    /// </summary>
    public class SmartyStreetsClient : ISmartyStreetsClient
	{
		ApiClient _client;
		string _authId;
		string _authToken;
		JsonSerializer _serializer;

		/// <summary>
		/// Initializes a new instance of the SmartyStreetsClient.
		/// It can (and should) be reused.
		/// 
		/// See http://smartystreets.com/kb/liveaddress-api/rest-endpoint for documentation
		/// on the keys you'll need. If these are not set in the constructor, 
		/// the client will attempt to retrieve them from an app.config or web.config. 
		/// Do not expose either of these keys or use them directly on a web page. 
		/// Keep them secret, keep them safe.
		/// </summary>
		/// <param name="authId">Unique "auth-id" value provided by SmartyStreets.</param>
		/// <param name="authToken">Unique "auth-token" value.</param>
		public SmartyStreetsClient(string authId, string authToken)
		{
			_client = ApiClient.Instance;
			_authId = authId;
			_authToken = authToken;
			_serializer = new JsonSerializer();
				
			if (string.IsNullOrWhiteSpace(_authId) || string.IsNullOrWhiteSpace(_authToken))
				throw new Exception(
					"Could not find one or either of the SmartyStreets auth keys.");
		}


		#region Deprecated
		/// <summary>
		/// Attempts to resolve a street address to a verified one.
		/// Makes requests to https://api.smartystreets.com/street-address.
		/// See http://smartystreets.com/kb/liveaddress-api/rest-endpoint for
		/// documentation on this endpoint.
		/// </summary>
		/// <param name="street">The street address, or a single-line (freeform) address.</param>
		/// <param name="city">The city name.</param>
		/// <param name="state">The state name.</param>
		/// <param name="zipcode">The ZIP code.</param>
		/// <returns>An enumerable list of possible addresses. Generally, one entry 
		/// will be returned, but results can include up to five possibles. If none are found,
		/// the array will be empty.</returns>
		[Obsolete("Use async method instead")]
		public Task<IEnumerable<SmartyStreetsAddress>> GetStreetAddress(
			string street = null, string secondary = null,
			string street2 = null, string city = null,
			string state = null, string zipcode = null)
		{
			return GetStreetAddressAsync(
				street, secondary, street2,
				city, state, zipcode);
		}

		/// <summary>
		/// Allows you to identify cities with ZIP codes, and vice-versa. 
		/// It also provides approximate geo-coordinates (latitude/longitude). 
		/// This endpoint does not support street addresses as input. See
		/// http://smartystreets.com/kb/liveaddress-api/zipcode-api for documentation
		/// on this endpoint.
		/// </summary>
		/// <param name="city">The city name.</param>
		/// <param name="state">The state name.</param>
		/// <param name="zip">The ZIP code.</param>
		/// <returns>An object with lists of matching Cities and States, along
		/// with any relevant zip codes that match the area. If SmartyStreets
		/// cannot find anything, an empty array will be returned.</returns>
		[Obsolete("Use async method instead")]
		public Task<IEnumerable<SmartyStreetsCityStateZipLookup>> GetLookup(
				string city = null, string state = null,
				string zip = null)
		{
			return GetLookupAsync(city, state, zip);
		}
		#endregion

		/// <summary>
		/// Attempts to resolve a street address to a verified one.
		/// Makes requests to https://api.smartystreets.com/street-address.
		/// See http://smartystreets.com/kb/liveaddress-api/rest-endpoint for
		/// documentation on this endpoint.
		/// </summary>
		/// <param name="street">The street address, or a single-line (freeform) address.</param>
		/// <param name="secondary">Apartment, suite,or office number.</param>
		/// <param name="street2">Any extra address info (e.g. *Leave it on the front porch*. 
		/// Note that this is *not* the second address line. Use "secondary" for that.</param>
		/// <param name="city">The city name.</param>
		/// <param name="state">The state name.</param>
		/// <param name="zipcode">The ZIP code.</param>
		/// <returns>An enumerable list of possible addresses. Generally, one entry 
		/// will be returned, but results can include up to five possibles. If none are found,
		/// the array will be empty.</returns>
		public async Task<IEnumerable<SmartyStreetsAddress>> GetStreetAddressAsync(
			string street = null, string secondary = null,
			string street2 = null, string city = null,
			string state = null, string zipcode = null)
		{
			var args = SetAuth();
			args["street"] = street;
			args["secondary"] = secondary;
			args["street2"] = street2;
			args["city"] = city;
			args["state"] = state;
			args["zipcode"] = zipcode;
			args["candidates"] = "5";

			var url = _client.CreateAddress("street-address", args);
			var response = await _client.PostAsync(url);

			//special cases
			if (response.Length == 3)
				return new SmartyStreetsAddress[0];

			using (var sr = new StreamReader(response))
			using (var jr = new JsonTextReader(sr))
				return _serializer.Deserialize<SmartyStreetsAddress[]>(jr) ?? new SmartyStreetsAddress[0];
		}

		/// <summary>
		/// Allows you to identify cities with ZIP codes, and vice-versa. 
		/// It also provides approximate geo-coordinates (latitude/longitude). 
		/// This endpoint does not support street addresses as input. See
		/// http://smartystreets.com/kb/liveaddress-api/zipcode-api for documentation
		/// on this endpoint.
		/// </summary>
		/// <param name="city">The city name.</param>
		/// <param name="state">The state name.</param>
		/// <param name="zip">The ZIP code.</param>
		/// <returns>An object with lists of matching Cities and States, along
		/// with any relevant zip codes that match the area. If SmartyStreets
		/// cannot find anything, an empty array will be returned.</returns>
		public async Task<IEnumerable<SmartyStreetsCityStateZipLookup>> GetLookupAsync(
			string city = null, string state = null, 
			string zip = null)
		{
			var args = SetAuth();
			args["city"] = city;
			args["state"] = state;
			args["zip"] = zip;

			var url = _client.CreateAddress("zipcode", args);
			var response = await _client.PostAsync(url);

			using (var sr = new StreamReader(response))
			using (var jr = new JsonTextReader(sr))
				return _serializer.Deserialize<SmartyStreetsCityStateZipLookup[]>(jr) ?? 
					new SmartyStreetsCityStateZipLookup[0];
		}

		/// <summary>
		/// Handles assignment of the keys necessary to talk to SmartyStreets.
		/// </summary>
		/// <param name="dict">A dictionary with any arguments intended to be 
		/// passed to SmartyStreets. If an existing dictionary is not supplied, 
		/// it will instantiate and return a new one.</param>
		/// <returns>A dictionary with the necessary keys to talk to SmartyStreeets.</returns>
		private Dictionary<string, string> SetAuth(Dictionary<string, string> dict = null)
		{
			if (dict == null)
				dict = new Dictionary<string, string>();

			dict["auth-id"] = _authId;
			dict["auth-token"] = _authToken;

			return dict;
		}
	}
}
