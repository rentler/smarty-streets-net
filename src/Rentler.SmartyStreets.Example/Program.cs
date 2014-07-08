using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentler.SmartyStreets.Example
{
	class Program
	{
		static void Main()
		{
			MainAsync().Wait();
		}

		static async Task MainAsync()
		{
			await StreetAddressLookup();
			await ZipLookup();
			Console.ReadKey();
		}

		static async Task StreetAddressLookup()
		{
			var client = new SmartyStreetsClient();
			var results = new List<SmartyStreetsAddress>();

			results.AddRange(
			(await client.GetStreetAddress(
				street: "125 nw 20th Apt 3",
				city: "portland",
				state: "or",
				zipcode: "")));

			results.AddRange(
			(await client.GetStreetAddress(
				street: "125 nw 20th Pl Apt 3",
				city: "portland",
				state: "or",
				zipcode: "97209")));

			results.AddRange(
			(await client.GetStreetAddress(
				street: "126 nw 20th Pl Apt 3",
				city: "portland",
				state: "or",
				zipcode: "97209")));

			results.AddRange(
			(await client.GetStreetAddress(
				street: "125 nw 20th Pl Apt 17",
				city: "portland",
				state: "or",
				zipcode: "")));

			foreach (var item in results)
			{
				var add = Address.FromSmartyStreetsAddress(item);
				Console.WriteLine(add.SafeUniqueAddress + " : " + add.UniqueHash);
			}
		}

		static async Task ZipLookup()
		{
			var client = new SmartyStreetsClient();
			var results = new List<SmartyStreetsCityStateZipLookup>();

			results.AddRange((await client.GetLookup(
				city: "portland",
				state: "or")));

			Console.WriteLine("portland");
			foreach (var i in results)
			{
				string cities = "";
				i.city_states.ForEach(c => cities += c.city + ", ");
				cities = cities.Trim().TrimEnd(',');
				Console.WriteLine(cities);

				string zipcodes = "";
				i.zipcodes.ForEach(z => zipcodes += z.zipcode + ", ");
				zipcodes = zipcodes.Trim().TrimEnd(',');
				Console.WriteLine(zipcodes);
			}
		}
	}
}
