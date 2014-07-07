using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvsConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			MainAsync().Wait();
		}

		static async Task MainAsync()
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
				var add = client.ToAddress(item);
				Console.WriteLine(add.SafeUniqueAddress + " : " + add.UniqueHash);
			}

			Console.ReadKey(true);
		}
	}
}
