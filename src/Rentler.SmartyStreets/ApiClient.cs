using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rentler.SmartyStreets
{
	/// <summary>
	/// A simple reusable Http client that handles
	/// managing a System.Net.HttpClient for basic
	/// GET/POST operations.
	/// </summary>
	public class ApiClient
	{
		HttpClient client = new HttpClient();

		static readonly Lazy<ApiClient> instance = new Lazy<ApiClient>();

		public ApiClient() { }

		public static ApiClient Instance
		{
			get { return instance.Value; }
		}

		/// <summary>
		/// Builds a Uri for a particular SmartyStreets endpoint.
		/// </summary>
		/// <param name="endpoint">Relative url of the endpoint.</param>
		/// <param name="args">Any arguments to be included in the url's querystring.</param>
		/// <returns>A proper Uri to be used in a GET or POST request.</returns>
		public Uri CreateAddress(string endpoint, Dictionary<string, string> args)
		{
			var url = "https://api.smartystreets.com/";

			var keys = args.Keys.ToArray();
			for (int i = 0; i < keys.Length; i++)
				args[keys[i]] = Uri.EscapeDataString(args[keys[i]]);

			url += endpoint + "?" + args.ToString("=", "&");
			return new Uri(url);
		}

		public async Task<Stream> Get(Uri url)
		{
			var result = await client.GetAsync(url);
			return await result.Content.ReadAsStreamAsync();
		}

		public async Task<Stream> Post(Uri url)
		{
			var result = await client.PostAsync(url, null);
			return await result.Content.ReadAsStreamAsync();
		}
	}
}
