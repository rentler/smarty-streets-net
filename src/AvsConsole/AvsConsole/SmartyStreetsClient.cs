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

		public async Task<IEnumerable<Address>> GetStreetAddress(
			string street = null, string city = null, 
			string state = null, string zipcode = null)
		{
			var args = SetAuth();
			args["street"] = street;
			args["city"] = city;
			args["state"] = state;
			args["zipcode"] = zipcode;

			var url = client.CreateAddress("street-address", args);
			var response = await client.Post(url);
			var obj = JsonSerializer.DeserializeFromStream<SmartyStreetsAddress[]>(response) 
						?? 
					  new SmartyStreetsAddress[]{};

			return ToAddress(obj);
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

		Dictionary<string,string> SetAuth(Dictionary<string,string> dict = null)
		{
			if (dict == null)
				dict = new Dictionary<string, string>();

			dict["auth-id"] = authId;
			dict["auth-token"] = authToken;

			return dict;
		}

		IEnumerable<Address> ToAddress(params SmartyStreetsAddress[] adds)
		{
			foreach (var item in adds)
			{
				yield return new Address
				{
					Address1 = string.Format("{0} {1} {2} {3} {4}",
					   item.components.primary_number,
					   item.components.street_predirection,
					   item.components.street_name,
					   item.components.street_suffix,
					   item.components.street_postdirection),
					Address2 = string.Format("{0} {1}",
						item.components.secondary_designator,
						item.components.secondary_number),
					City = item.components.default_city_name ?? item.components.city_name,
					State = item.components.state_abbreviation,
					Zip = item.components.zipcode
				};
			}
		}
	}
}
