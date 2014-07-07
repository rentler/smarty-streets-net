using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvsConsole
{
	public class SmartyStreetsClient
	{
		ApiClient client;
		string authId;
		string authToken;

		public SmartyStreetsClient(
			string authId = null,
			string authToken = null)
		{
			client = ApiClient.Instance;
			this.authId = authId ?? App.SmartyStreetsAuthId;
			this.authToken = authToken ?? App.SmartyStreetsAuthToken;
		}

		public async Task<IEnumerable<SmartyStreetsAddress>> GetStreetAddress(
			string street = null, string city = null,
			string state = null, string zipcode = null)
		{
			var args = SetAuth();
			args["street"] = street;
			args["city"] = city;
			args["state"] = state;
			args["zipcode"] = zipcode;
			args["candidates"] = "5";

			var url = client.CreateAddress("street-address", args);
			var response = await client.Post(url);
			var obj = JsonSerializer.DeserializeFromStream<SmartyStreetsAddress[]>(response)
						??
					  new SmartyStreetsAddress[] { };

			return obj;
		}

		public async Task<IEnumerable<SmartyStreetsCityStateZipLookup>> GetLookup(
			string city = null, string state = null,
			string zip = null)
		{
			var args = SetAuth();
			args["city"] = city;
			args["state"] = state;
			args["zip"] = zip;

			var url = client.CreateAddress("zipcode", args);
			var response = await client.Post(url);
			var obj = JsonSerializer.DeserializeFromStream<SmartyStreetsCityStateZipLookup[]>(response)
						??
					  new SmartyStreetsCityStateZipLookup[] { };

			return obj;
		}

		Dictionary<string, string> SetAuth(Dictionary<string, string> dict = null)
		{
			if (dict == null)
				dict = new Dictionary<string, string>();

			dict["auth-id"] = authId;
			dict["auth-token"] = authToken;

			return dict;
		}

		public Address ToAddress(SmartyStreetsAddress add)
		{
			return new Address
			{
				Address1 = string.Format("{0} {1} {2} {3} {4}",
				   add.components.primary_number,
				   add.components.street_predirection,
				   add.components.street_name,
				   add.components.street_suffix,
				   add.components.street_postdirection).Replace("  ", " ").Trim(),
				Address2 = string.Format("{0} {1}",
					add.components.secondary_designator,
					add.components.secondary_number).Replace("  ", " ").Trim(),
				City = add.components.default_city_name ?? add.components.city_name,
				State = add.components.state_abbreviation,
				Zip = add.components.zipcode
			};
		}

		public IEnumerable<Address> ToAddress(SmartyStreetsAddress[] adds)
		{
			foreach (var add in adds)
				yield return ToAddress(add);
		}
	}
}
