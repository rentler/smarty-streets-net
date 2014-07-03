using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

			//var results =
			//	(await client.GetStreetAddress(
			//		street: "274 s 16th",
			//		city: "st helens",
			//		state: "or",
			//		zipcode: "")).ToList();

			var results =
			(await client.GetStreetAddress(
				street: "5185 S 900 E",
				city: "slc",
				state: "ut",
				zipcode: "")).ToList();

			results.ForEach(r => Console.WriteLine(r.FullAddress));

			var lookups =
				(await client.GetLookup(
					city: "slc",
					state: "ut")).ToList();

			Console.ReadKey(true);
		}
	}
}
